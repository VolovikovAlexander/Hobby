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
        private readonly IAccuralsFactory _factory;


        public TestBL()
        {
            _services = new ServiceCollection();
            _services.AddAccuralsServices();

            _provider = _services.BuildServiceProvider();
            _context = (_provider.GetRequiredService<ICompany>() as Company)
                        .CreateContext();

            // ������� ��� �������
            _factory = _provider.GetRequiredService<IAccuralsFactory>();
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

            // ��������
            var result = _factory.Process(emploee, DateTime.Now);

            // ��������
            Assert.IsNotNull(result);
            Assert.AreEqual(true, result.Any());
            Assert.AreEqual(true, result.Any(x => x.Emploee.Id == emploee.Id));

            var item = result.Where(x => x.Emploee.Id == emploee.Id);
            Assert.AreEqual(true, item.All(x => x.Cost == AccuralsHelper.Intern));
        }

        /// <summary>
        /// ��������� ������ ���������� ����� �� ���������
        /// </summary>
        [Test]
        public void Check_Accurals_Manager()
        {
            // ����������
            var emploee = _context.Departments.First().Boss;

            // ��������
            var result = _factory.Process(emploee, DateTime.Now);

            // ��������
            Assert.IsNotNull(result);
            Assert.AreEqual(true, result.Any());
            Assert.AreEqual(true, result.Any(x => x.Emploee.Id == emploee.Id));

            var item = result.Where(x => x.Emploee.Id == emploee.Id);
            Assert.AreEqual(true, item.All(x => x.Cost == AccuralsHelper.Manager));
        }

        /// <summary>
        /// ��������� ������ ���������� ����� �� �������� ����������
        /// </summary>
        [Test]
        public void Check_Accurals_Clerk()
        {
            // ����������
            var emploee = _context
                           .Departments
                           .SelectMany(x => x.Emploees)
                           .Where(x => x.Type == EmploeeType.Clerk)
                           .First();

            // ���������
            var result = _factory.Process(emploee, DateTime.Now);

            // ��������
            Assert.IsNotNull(result);
            Assert.AreEqual(true, result.Any());
            Assert.AreEqual(true, result.Any(x => x.Emploee.Id == emploee.Id));

            // ������������ ������
            var items = result.Where(x => x.Emploee.Id == emploee.Id);

            // ����������� ������
            var periods = AccuralsHelper.GetPeriod(DateTime.Now);
            var workPeriods = AccuralsHelper.GetWorkPeriod(periods.Item1, periods.Item2);

            Assert.AreEqual(items.Sum(x => x.Cost), workPeriods.Count() * AccuralsHelper.Clerk);
        }

        /// <summary>
        /// ��������� ������ ���������, ������ ��������. ������� �� ����������� ���������� ����� �� ���� ����������� �������������.
        /// </summary>
        [Test]
        public void Check_Accurals_Manager_Scenario1()
        {
            // ����������
            var department = _context
                            .Departments
                            .Where(x => x.Emploees.Any())
                            .First();
            var manager = department.Boss;
            var period = DateTime.Now;
            var list = department.Emploees.Where(x => x.Type == EmploeeType.Clerk).ToList();

            // ��������
            // 1. ��� ������� ���������� �������� ������ ���������� �����
            foreach (var item in list)
            {
                var costs = _factory.Process(item, period);
                var tariff = (costs.First() as Accurals).ToAccuralsTariff();
                item.Tariffs.Add(period, tariff);
            }

            department.Emploees = list;

            // 2. ��� ������������ �������� ������ ���������� �����
            var result = _factory.Process(manager, period);

            // ��������
            Assert.IsNotNull(result);
            Assert.AreEqual(true, AccuralsHelper.Manager < result.Sum(x => x.Cost));
        }
    }
}