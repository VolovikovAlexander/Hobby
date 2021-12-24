using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson11.BL
{
    public static class MicrosoftExtensions
    {
        private static Random _rnd = new Random();

        /// <summary>
        /// Сформировать конекст с данными (произвольные)
        /// </summary>
        /// <param name="source"> Исходные данные с организацией </param>
        /// <returns></returns>
        public static ICompany CreateContext(this Company source)
        {
            // Создаем 100 разных сотрудников 
            var emploees = new List<IEmploee>();
            Enumerable.Range(1, 100)
                .ToList()
                .ForEach(x =>
                {
                    var emploee = new Emploee().CreateContextEmploee();
                    emploees.Add(emploee);
                });

            // Создаем 5 отделов до 100 сотрудников
            var departments = new List<IDepartment>();
            emploees.Where(x => x.Type == Enums.EmploeeType.Manager)
                        .Take(5)
                        .ToList()
                        .ForEach(x =>
                        {
                            var department = new Department()
                            {
                                Boss = x,
                                Description = _rnd.Next(1, 9999).ToString(),
                                Id = _rnd.Next(1, 9999),
                                Emploees = Enumerable.Range(1, 100)
                                              .Select(x => new Emploee().CreateContextEmploee())
                                              // Только рядовые сотрудники и интерны
                                              .Where(x => x.Type != Enums.EmploeeType.Manager)
                            };
                            departments.Add(department);
                        });

            // Параметры организации
            source.Departments = departments;
            source.Description = _rnd.Next(1, 9999).ToString();
            source.Id = _rnd.Next(1, 9999);


            // Формируем список 
            return source;
        }

        /// <summary>
        /// Сформироваь тестовый документ
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private static IDocument CreateContextDocument(this Document source)
        {
            source.Id = _rnd.Next(1, 9999);
            source.Number = _rnd.Next(1, 9999).ToString();
            source.Serial = _rnd.Next(1, 9999).ToString();
            return source;
        }

        /// <summary>
        /// Сформировать тестового сотрудника
        /// </summary>
        /// <param name="source"> Исходный сотрудник </param>
        /// <returns></returns>
        private static IEmploee CreateContextEmploee(this Emploee source)
        {
            source.FirstName = _rnd.Next(1, 9999).ToString();
            source.LastName = _rnd.Next(1, 9999).ToString();
            source.LastName = _rnd.Next(1, 9999).ToString();
            source.PhoneNumber = _rnd.Next(1, 9999).ToString();
            source.Id = _rnd.Next(1, 9999);

            var documents = new List<IDocument>();
            Enumerable.Range(1, 3)
                .ToList()
                .ForEach(x =>
                {
                    documents.Add(new Document().CreateContextDocument());
                });

            source.Documents = documents;

            // Тип сотрудника
            source.Type = (Enums.EmploeeType)Enum.Parse(typeof(Enums.EmploeeType), _rnd.Next(1, 4).ToString());
            return source;
        }

        

        /// <summary>
        /// Подключить сервисы для расчета заработной платы
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static IServiceCollection AddAccuralsServices(this IServiceCollection collection) =>
            collection
                 .AddSingleton<ICompany, Company>()
                 .AddSingleton<IAccuralsFactory, AccuralsFactory>();


        /// <summary>
        /// Получить структуру с информацией о заработной плате.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static AccuralsTariff ToAccuralsTariff(this Accurals source)
            => new AccuralsTariff()
                { Cost = source.Cost, Description = $"Начисление для {source.Emploee.ToString()} за {source.Period}" };

    }
}
