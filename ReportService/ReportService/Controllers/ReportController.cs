using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using ReportService.Domain;

namespace ReportService.Controllers
{
    using Microsoft.Extensions.Configuration;

    [Route("api/[controller]")]
    public class ReportController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IConfigurationSection _settingsSection;

        public ReportController(IConfiguration localConfig)
        {
            _config = localConfig;

            // Get setting from appsettings.json
            var _settingsSection = _config.GetSection("Settigs");
        }

        [HttpGet]
        [Route("{year}/{month}")]
        public async Task<ActionResult> Download(int year, int month)
        {
            #region "Checking correct params"
            if (year <= 0 || (month <= 0 || month > 12))
                return BadRequest();

            if(_settingsSection == null)
                return NoContent();
            #endregion

            #region "Prepare settings"

            var connectionString = _settingsSection.GetValue<string>("ConnectionString");
            if(string.IsNullOrEmpty(connectionString))
                return NoContent();

            var hostSallary = _settingsSection.GetValue<string>("HostSallary");
            if (string.IsNullOrEmpty(connectionString))
                return NoContent();

            var hostCode = _settingsSection.GetValue<string>("HostCode");
            if (string.IsNullOrEmpty(hostCode))
                return NoContent();

            #endregion

            #region "Creating report"

            var hosts = new Dictionary<string, string>()
            {
                {"Sallary", hostSallary },
                {"Code", hostCode }
            };

            var report = "";
            var reportTemplate = "{0}\n===============================\n{1}=============================\n{2}====================\nThank you)!";
            var employees = await ServiceHelper.GetAndPrepareEmployees(connectionString, hosts);
            if(employees.Any())
                report = ServiceHelper.BuildReport(employees, reportTemplate, year, month);

            #endregion

            #region "Creating response"

            using (var stream = new MemoryStream())
            {
                var writer = new StreamWriter(stream);
                writer.Write(report);


                writer.Flush();
                stream.Position = 0;

                var response = File(stream, "application/octet-stream", "report.txt");
                return response;
            }

            #endregion
        }
    }
}
