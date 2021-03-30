using ControlPoint.Core;
using ControlPoint.Models;
using NUnit.Framework;
using System;
using System.Linq;

namespace ControlPoint.Tests
{
    public class DbBaseContextTests
    {
        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// Проверка подключения к базе данных
        /// </summary>
        [Test]
        public void Connect()
        {
            try
            {
                var context = new ControlPointDbContext();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Fail();
            }
        }

        /// <summary>
        /// Последовательная проверка выборки данных
        /// </summary>
        [Test]
        public void CheckGetTableData()
        {
            using (var context = new ControlPointDbContext() )
            {
                var top1_10 = context.Table1.Take(10);
                if (top1_10.Count() != 10)
                    Assert.Fail("Не возможно получить данные из таблицы Table1");

                var top2_10 = context.Table2.Take(10);
                if (top2_10.Count() != 10)
                    Assert.Fail("Не возможно получить данные из таблицы Table2");

                var top3_10 = context.Table1.Take(10);
                if (top3_10.Count() != 10)
                    Assert.Fail("Не возможно получить данные из таблицы Table3");
            }
        }

        /// <summary>
        /// Проверка записи в таблицы
        /// </summary>
        [Test]
        public void CheckSaveTableData()
        {
            using (var context = new ControlPointDbContext())
            {
                var item1 = new Table1()
                { Data = "Test1" };

                context.Table1.Add(item1);

                var item2 = new Table2()
                { Data = "Test2" };

                context.Table2.Add(item2);

                var item3 = new Table3()
                { Data = "Test3" };

                context.Table3.Add(item3);

                context.SaveChanges();
            }
        }
    }
}