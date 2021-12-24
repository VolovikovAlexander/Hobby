using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace Lesson11.BL
{
    /// <summary>
    /// Класс для формирования отчета по начислению
    /// </summary>
    public static class ReportHelper
    {
        /// <summary>
        /// Сформировать заголовок отчета
        /// </summary>
        /// <returns></returns>
        public static string CreateReport(ICompany source)
        {
            var builder = new StringBuilder();
            builder.Append("======================================================\n");
            builder.Append("* Edit by valex from 2021-12-24                      *\n");
            builder.Append("*    - Пример реализации задачи по начислению        *\n");
            builder.Append("*      заработной платы разным категориям сотрудников*\n");
            builder.Append("*====================================================*\n");
            builder.Append("*    - C#, LINQ, MS DI                               *\n");
            builder.Append("*====================================================*\n");
            builder.Append("");

            foreach (var department in source.Departments)
            {
                builder.Append($"Подразделение: {department.Description}, руководитель {department.Boss.ToString()}, начислено {department.Boss.Tariffs.Values.Sum(x => x.Cost)}\n");
                builder.Append("*====================================================*\n");

                foreach (var emploee in department.Emploees)
                    builder.Append($"   - сотрудник : {emploee.ToString()}, тип {emploee.Type}, начислено {emploee.Tariffs.Values.Sum(x => x.Cost)}\n");

                builder.Append("*====================================================*\n");
                var total = department.Emploees.SelectMany(x => x.Tariffs).Sum(x => x.Value.Cost);
                builder.Append($"Итого по подразделению : {total}\n");
                builder.Append("*====================================================*\n");
            }

            return builder.ToString();
        }
    }
}
