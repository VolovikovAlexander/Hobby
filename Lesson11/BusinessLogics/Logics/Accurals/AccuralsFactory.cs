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
            if (context is null)
                throw new ArgumentNullException("Некоректно переданы параметры!", nameof(context));


            Startup(context);
        }

        #region IAccuralsFactory

        /// <summary>
        /// Провести полный расчет заработной платы
        /// </summary>
        /// <param name="emploee"></param>
        /// <param name="period"></param>
        /// <returns></returns>
        public IEnumerable<IProccessAccurals> Process(DateTime period)
        {
            var emploees = _context
                                .Departments
                                .SelectMany(x => x.Emploees);

            var result = new List<IProccessAccurals>();
            foreach(var item in emploees)
            {
                var costs = Process(item, period);
                result.AddRange(costs.ToArray());
            }

            return result;
        }

        /// <summary>
        /// Подключить обработчики для начисления заработной платы
        /// </summary>
        /// <param name="context"></param>
        public void Startup(ICompany context)
        {
            _context = context;

            // Подключаем сервисы
            IServiceCollection services = new ServiceCollection();
            services.AddTransient<IProccessAccurals, ClerkProccessAccurals>();
            services.AddTransient<IProccessAccurals, InternProccessAccurals>();
            services.AddTransient<IProccessAccurals, ManagerProccessAccurals>();

            // Формируем провайдер
            _provider = services.BuildServiceProvider();

            // Подключаем сервисы к контексту
            foreach (var item in _provider.GetServices<IProccessAccurals>())
                item.Context = _context;
        }

        #endregion

        /// <summary>
        /// Провести расчета заработной платы по одному сотруднику.
        /// </summary>
        /// <param name="emploee"> Сотрудник </param>
        /// <param name="period"> Период </param>
        /// <returns></returns>
        private IEnumerable<IProccessAccurals> Process(IEmploee emploee, DateTime period)
        {
            return null; 
        }
    }
}
