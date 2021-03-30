using ControlPoint.Core;
using ControlPoint.Models;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ControlPoint.Tests
{
    public class BuisinesLogicTest
    {
        /// <summary>
        /// Проверка генерации и вставки в таблицу 1
        /// </summary>
        [Test]
        public void CheckExecuteTable1Method()
        {
            var main = new BuildProcess<Table1>();
            long lastID = 0;
            using (var context = new ControlPointDbContext())
            {
                lastID = context.Table1.OrderBy(x => x.ID).LastOrDefault().ID;
            }

            // Формируем 100 произвольных записей
            var task = Task.Run(() =>
            {
                main.Execute(100);
            });

            while (!task.IsCompleted) { }

            if (task.Status == TaskStatus.Faulted)
            {
                var message = string.Join(Environment.NewLine, task.Exception.InnerExceptions.Select(x => x.Message + x.InnerException?.Message).ToArray());
                Console.WriteLine(message);
                Assert.Fail();
            }

            using (var context = new ControlPointDbContext())
            {
                // Проверяем результат. Должна быть вставка 100 записей
                var countRecs = context.Table1.Where(x => x.ID > lastID).Count();
                Assert.IsTrue(countRecs == 100, "Нет вставки данных!");
            }
        }

        /// <summary>
        /// Проверка генерации и встиавки в таблицу 2
        /// </summary>
        [Test]
        public void CheckExecuteTable2Method()
        {
            var main = new BuildProcess<Table2>();
            long lastID = 0;
            using (var context = new ControlPointDbContext())
            {
                lastID = context.Table2.OrderBy(x => x.ID).LastOrDefault().ID;
            }

            // Формируем 100 произвольных записей
            var task = Task.Run(() =>
            {
                main.Execute(100);
            });

            while (!task.IsCompleted) { }

            if (task.Status == TaskStatus.Faulted)
            {
                var message = string.Join(Environment.NewLine, task.Exception.InnerExceptions.Select(x => x.Message + x.InnerException?.Message).ToArray());
                Console.WriteLine(message);
                Assert.Fail();
            }

            using (var context = new ControlPointDbContext())
            {
                // Проверяем результат. Должна быть вставка 100 записей
                var countRecs = context.Table2.Where(x => x.ID > lastID).Count();
                Assert.IsTrue(countRecs == 100, "Нет вставки данных!");
            }
        }

        /// <summary>
        /// Проверка генерации и встиавки в таблицу 2
        /// </summary>
        [Test]
        public void CheckExecuteTable3Method()
        {
            var main = new BuildProcess<Table3>();
            long lastID = 0;
            using (var context = new ControlPointDbContext())
            {
                lastID = context.Table3.OrderBy(x => x.ID).LastOrDefault().ID;
            }

            // Формируем 100 произвольных записей
            var task = Task.Run(() =>
            {
                main.Execute(100);
            });

            while (!task.IsCompleted) { }

            if (task.Status == TaskStatus.Faulted)
            {
                var message = string.Join(Environment.NewLine, task.Exception.InnerExceptions.Select(x => x.Message + x.InnerException?.Message).ToArray());
                Console.WriteLine(message);
                Assert.Fail();
            }

            using (var context = new ControlPointDbContext())
            {
                // Проверяем результат. Должна быть вставка 100 записей
                var countRecs = context.Table3.Where(x => x.ID > lastID).Count();
                Assert.IsTrue(countRecs == 100, "Нет вставки данных!");
            }
        }

        /// <summary>
        /// Проверка работы асинхронного метода генерации
        /// </summary>
        /// <returns></returns>
        [Test]
        public void CheckExecuteTable1Async()
        {
            var main = new BuildProcess<Table1>();
            long lastID = 0;
            using (var context = new ControlPointDbContext())
            {
                lastID = context.Table3.OrderBy(x => x.ID).LastOrDefault().ID;
            }

            // Вызываем ассинхронный метод
            var result = main.ExecuteAsync(200).Result;
            Assert.AreEqual(result, 200);

            using (var context = new ControlPointDbContext())
            {
                // Проверяем результат. Должна быть вставка 100 записей
                var countRecs = context.Table1.Where(x => x.ID > lastID).Count();
                Assert.IsTrue(countRecs == 200, "Нет вставки данных!");
            }
        }
    }
}
