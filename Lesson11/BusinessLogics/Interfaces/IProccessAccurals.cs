using Lesson11.BL.Enums;
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
        /// Минимальная ставка
        /// </summary>
        public double MinimalCost { get; set; }

        /// <summary>
        /// Сформировать начисление 
        /// </summary>
        /// <param name="emploee"> Сотрудник </param>
        /// <param name="period"> Период </param>
        /// <returns></returns>
        public IEnumerable<IAccruals> Proccess(ICompany context, IEmploee emploee, DateTime period);

        /// <summary>
        /// Тип сотрудника
        /// </summary>
        public EmploeeType Type { get; }
    }
}
