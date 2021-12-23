using System;
using System.Collections.Generic;

namespace Lesson11.BL
{
    /// <summary>
    /// Интерфейс к фабрики для расчета заработной платы
    /// </summary>
    public interface IAccuralsFactory
    {
        /// <summary>
        /// Провести расчет заработной платы
        /// </summary>
        /// <param name="period"> Период </param>
        /// <returns></returns>
        public IEnumerable<IProccessAccurals> Process(DateTime period);

        /// <summary>
        /// Сконфигурировать фабрику
        /// </summary>
        /// <param name="Context"> Контекст с данными </param>
        public void Startup(ICompany Context);
    }
}
