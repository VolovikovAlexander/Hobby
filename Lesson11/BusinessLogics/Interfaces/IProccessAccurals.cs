using System;
using System.Collections.Generic;

namespace Lesson11.BL
{
    /// <summary>
    /// Интерфейс для орведения начисления заработной платы
    /// </summary>
    public interface IProccessAccurals
    {
        /// <summary>
        /// Контекст для работы с данными
        /// </summary>
        public ICompany Context { set; }

        /// <summary>
        /// Минимальная ставка
        /// </summary>
        public double MinimalCost { get; set; }

        /// <summary>
        /// Сформировать начисление 
        /// </summary>
        /// <param name="emploee"> Сотрудник </param>
        /// <param name="period"> Период </param>
        /// <returns></returns>
        public IEnumerable<IAccruals> Proccess(IEmploee emploee, DateTime period);
    }
}
