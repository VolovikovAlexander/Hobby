using Lesson11.BL;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Linq;
using Lesson11.BL.Enums;

namespace Tests
{
    /// <summary>
    /// Набор для проверки бизнес логики
    /// </summary>
    public class TestBL
    {
        private readonly ICompany _context;
        private readonly IServiceCollection _services;
        private readonly IServiceProvider _provider;


        public TestBL()
        {
            _services = new ServiceCollection();
            _services.AddAccuralsServices();

            _provider = _services.BuildServiceProvider();
            _context = (_provider.GetRequiredService<ICompany>() as Company)
                        .CreateContext();
        }

        /// <summary>
        /// Проверить расчет заработной платы по интерну
        /// </summary>
        [Test]
        public void Check_Accurals_Intern()
        {
            // Подготовка
            var emploee = _context
                                .Departments
                                .SelectMany(x => x.Emploees)
                                .Where(x => x.Type == EmploeeType.Intern)
                                .First();
            var factory = _provider.GetRequiredService<IAccuralsFactory>();


            // Действие
            var result = factory.Process(DateTime.Now);

            // Проверки
            Assert.IsNotNull(result);
            Assert.AreEqual(true, result.Any());
            Assert.AreEqual(true, result.Any(x => x.Emploee.Id == emploee.Id));

            var item = result.Where(x => x.Emploee.Id == emploee.Id);
            Assert.AreEqual(true, item.All(x => x.Cost > 0));
        }

        /// <summary>
        /// Проверить расчета заработной платы по менеджеру
        /// </summary>
        [Test]
        public void Check_Accurals_Manager()
        {

        }
    }
}