using Lesson11.BL;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Lesson11
{
    class Program
    {
        static void Main(string[] args)
        {
            // Подключаем сервисы
            var services = new ServiceCollection();
            services.AddAccuralsServices();

            // Получаем провайдера
            var provider = services.BuildServiceProvider();

            // Формируем контектс с данными
            var context = (provider.GetRequiredService<ICompany>() as Company)
                        .CreateContext();

            // Производит расчет заработной платы по организации
            var factory = provider.GetRequiredService<IAccuralsFactory>();
            var result = factory.Process(DateTime.Now);

            // Выводим результат
            Console.WriteLine(ReportHelper.CreateReport(context));
            Console.ReadLine();
        }
    }
}
