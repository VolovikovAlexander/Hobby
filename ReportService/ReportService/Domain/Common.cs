using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Dapper;

namespace ReportService.Domain
{
    using Npgsql;
    using System.Collections.Concurrent;
    using System.Net.Http;

    public static class ServiceHelper
    {
        /// <summary>
        /// Getting the inner code Employee from other service
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="sourceHost"></param>
        /// <returns></returns>
        private static async Task<string> GetCode(Employee employee, string sourceHost)
        {
            if (employee == null || string.IsNullOrEmpty(sourceHost))
                return "";

            var host = string.Format("{0}{1}", sourceHost, employee.Inn);
            var client = new HttpClient();
            return await client.GetStringAsync(host);
        }

        /// <summary>
        /// Getting sallary for Employee from other service
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="sourceHost"></param>
        /// <returns></returns>
        private static int Salary(Employee employee, string sourceHost)
        {
            // Check correct
            if (employee == null || string.IsNullOrEmpty(sourceHost))
                return -1;

            // Тут по хорошму можно переписать и заменить на HttpClient
            // но но вид это долно нормально работать.

            var host = string.Format("{1}{2}", sourceHost, employee.Inn);
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(host);

            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(new { employee.BuhCode });
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            var reader = new System.IO.StreamReader(httpResponse.GetResponseStream(), true);
            string responseText = reader.ReadToEnd();

            // Check correct
            if (!string.IsNullOrEmpty(responseText))
                return -1;

            int result = 0;
            int.TryParse(responseText, out result);
            return result;
        }

        /// <summary>
        /// Getting the actual emploees
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        private static async Task<IList<Employee>> GetActualEmployeesAsync(string connectionString)
        {
            var result = new List<Employee>();
            if (string.IsNullOrEmpty(connectionString))
                return result;

            var query = @"
                with cte_departs as
                (
                    -- Actual departments only
	                SELECT d.name from deps d where d.active = true
                ),
                cte_report as
                (
                    Select e.name as Name, e.inn as Inn, d.name as Department from emps e
	                inner join cte_departs as d on e.departmentid = d.id
                    -- For exluded dublicate
                    group by e.name ,  e.inn, d.name
                );

                Select Name, Inn, Department from cte_report order by Department, Name";

            using (var connect = new NpgsqlConnection(connectionString))
            {
                await connect.OpenAsync();
                var items = connect.Query<Employee>(query);
                if (items.Any())
                    result.AddRange(items);
            }

            return result;
        }

        /// <summary>
        /// Preparing list of Employee. Getting code and sallary from other services.
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static async Task<IList<Employee>> GetAndPrepareEmployees(string connectionString, IDictionary<string, string> sourceHosts)
        {
            IList<Employee> result = new List<Employee>();
            result = await GetActualEmployeesAsync(connectionString);

            if (result == null)
                return result;

            if (!result.Any())
                return result;

            if (!sourceHosts.Any())
                return result;

            var tasks = new ConcurrentBag<Task>();
            result.ToList()
                .ForEach(x =>
                {
                    var task = Task.Run(() =>
                    {
                        var employee = x;

                        // Get a special code form service
                        var codeHost = (sourceHosts.Keys.Contains("Code") ? sourceHosts["Code"] : "");
                        employee.BuhCode = ServiceHelper.GetCode(employee, codeHost).Result;

                        // Get salary from service
                        var salaryHost = (sourceHosts.Keys.Contains("Salary") ? sourceHosts["Salary"] : "");
                        employee.Salary = ServiceHelper.Salary(employee, salaryHost);
                    });

                    tasks.Add(task);
                });


            Task.WaitAll(tasks.ToArray());
            return result;
        }

        /// <summary>
        /// Creating new report and placed it into stream
        /// </summary>
        /// <param name="sourceItems"></param>
        /// <returns></returns>
        public static string BuildReport(IList<Employee> sourceItems, string template, int year, int month)
        {
            //  Checking a correct params
            if (sourceItems == null) throw new Exception("Argument wrong!");
            if (!sourceItems.Any()) return "";
            if (string.IsNullOrEmpty(template)) throw new Exception("Argument wrong!");

            if (year <= 0 || (month <= 0 || month > 12))
                return "";

            // Getting totals
            var departments = sourceItems.Select(x => new { x.Department, x.Salary })
                .GroupBy(x => x.Department)
                .Select(x => new
                {
                    Department = x.Key,
                    TotalSalary = x.Sum(y => y.Salary)
                }).ToList();

            var list = departments.Select(x => x.Department).Distinct().OrderBy(x => x)
                .ToList();

            var period = new DateTime(year, month, 1);
            var header = $"ФИНАНСОВЫЙ ОТЧЕТ\n\n{period.ToString("MMMM yyyy")}";
            var body = "";

            // Creating report of template
            list.ForEach(x =>
            {
                body += $"Департамент {x}\n";
                var employees = sourceItems.Where(y => y.Department == x)
                            .Select(y => y.ToString()).ToArray();
                body+= string.Join("\n", employees);
                body += "\n";

                // Print subtotal for each department
                body += $"Итого {departments.Where(y => y.Department == x).FirstOrDefault().TotalSalary} руб.\n";
            });

            // Print total
            var total = $"\nИТОГО: {departments.Sum(x => x.TotalSalary)} руб.\n";

            // TODO: Тут по хорошему стоит проверить наличие 3-ех параемтров в шаблоне!
            return string.Format(template, header, body, total);
        }
    }
}
