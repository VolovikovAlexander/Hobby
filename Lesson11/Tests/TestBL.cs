using Lesson11.BL;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Linq;
using Lesson11.BL.Enums;

namespace Tests
{
    /// <summary>
    /// ����� ��� �������� ������ ������
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
        /// ��������� ������ ���������� ����� �� �������
        /// </summary>
        [Test]
        public void Check_Accurals_Intern()
        {
            // ����������
            var emploee = _context
                                .Departments
                                .SelectMany(x => x.Emploees)
                                .Where(x => x.Type == EmploeeType.Intern)
                                .First();
            var factory = _provider.GetRequiredService<IAccuralsFactory>();


            // ��������
            var result = factory.Process(DateTime.Now);

            // ��������
            Assert.IsNotNull(result);
            Assert.AreEqual(true, result.Any());
            Assert.AreEqual(true, result.Any(x => x.Emploee.Id == emploee.Id));

            var item = result.Where(x => x.Emploee.Id == emploee.Id);
            Assert.AreEqual(true, item.All(x => x.Cost > 0));
        }

        /// <summary>
        /// ��������� ������� ���������� ����� �� ���������
        /// </summary>
        [Test]
        public void Check_Accurals_Manager()
        {

        }
    }
}