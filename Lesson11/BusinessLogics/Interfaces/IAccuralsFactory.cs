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
        public IEnumerable<IAccruals> Process(DateTime period);

        /// <summary>
        /// Сконфигурировать фабрику
        /// </summary>
        public void Startup();
    }
}
