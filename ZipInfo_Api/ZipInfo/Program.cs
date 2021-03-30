using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using LinqToDB.Data;

namespace ZipInfo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Подключаем базу данных
            DataConnection.DefaultSettings = new ZipInfo.Core.ZipInfoSettings();
            // Запускаем API
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
