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
        /// Провести полный расчет заработной платы
        /// </summary>
        /// <param name="period"> Период </param>
        /// <returns></returns>
        public IEnumerable<IAccruals> Process(DateTime period);

        /// <summary>
        /// Провести расчета заработной платы по конкретному сотруднику
        /// </summary>
        /// <param name="emploee"> Сотрудник </param>
        /// <param name="period"> Период </param>
        /// <returns></returns>
        public IEnumerable<IAccruals> Process(IEmploee emploee, DateTime period);
    }
}
