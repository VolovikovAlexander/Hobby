using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson11.BL
{
    /// <summary>
    /// Фабрика для расчета заработной платы по сотрудникам.
    /// </summary>
    public class AccuralsFactory : IAccuralsFactory
    {
        private ServiceProvider _provider;
        private ICompany _context;

        /// <summary>
        /// Конструктор с переданным контекстом
        /// </summary>
        /// <param name="context"> Контекст с данными <see cref="ICompany"/></param>
        public AccuralsFactory(ICompany context)
        {
            _context = context ?? throw new ArgumentNullException("Некоректно переданы параметры!", nameof(context));
            Startup();
        }

        #region IAccuralsFactory

        /// <summary>
        /// Провести полный расчет заработной платы
        /// </summary>
        /// <param name="emploee"></param>
        /// <param name="period"></param>
        /// <returns></returns>
        public IEnumerable<IAccruals> Process(DateTime period)
        {
            var emploees = _context
                           .Departments
                           .SelectMany(x => x.Emploees);

            var result = new List<IAccruals>();
            emploees.ToList()
                .ForEach(x =>
                {
                    result.AddRange(Process(x, period).ToArray());
                });

            return result;
        }

        /// <summary>
        /// Подключить обработчики для начисления заработной платы
        /// </summary>
        /// <param name="context"></param>
        public void Startup()
        {
            // Подключаем сервисы
            IServiceCollection services = new ServiceCollection();
            services.AddTransient<IProccessAccurals, ClerkProccessAccurals>();
            services.AddTransient<IProccessAccurals, InternProccessAccurals>();
            services.AddTransient<IProccessAccurals, ManagerProccessAccurals>();

            // Формируем провайдер
            _provider = services.BuildServiceProvider();
        }

        #endregion

        /// <summary>
        /// Провести расчета заработной платы по одному сотруднику.
        /// </summary>
        /// <param name="emploee"> Сотрудник </param>
        /// <param name="period"> Период </param>
        /// <returns></returns>
        private IEnumerable<IAccruals> Process(IEmploee emploee, DateTime period)
        {
            var services = _provider.GetServices<IProccessAccurals>();
            if (!services.Any(x => x.Type == emploee.Type))
                throw new InvalidOperationException($"Невозможно произветсти расчет по сотруднику {emploee.ToString()}. Тип сотрудника: {emploee.Type} не включен в схему расчета!");

            return services.First(x => x.Type == emploee.Type).Proccess(_context, emploee, period);
        }
    }
}
