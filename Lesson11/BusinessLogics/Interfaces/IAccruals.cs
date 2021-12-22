using Lesson11.BL.Enums;
using System;

namespace Lesson11.BL
{
    /// <summary>
    /// Интерфейс к системе начисления
    /// </summary>
    public interface IAccruals: IEntity
    {
        /// <summary>
        /// Период начисления
        /// </summary>
        public DateTime Period { get; set; }
        
        /// <summary>
        /// Сотрудник
        /// </summary>
        public IEmploee Emploee { get; set; } 

        /// <summary>
        /// Сумма начисления
        /// </summary>
        public double Cost { get; set; }

        /// <summary>
        /// Тип сотрудника при начислении
        /// </summary>
        public EmploeeType EmploeeType { get; set; }

        /// <summary>
        /// Тип начисления
        /// </summary>
        public AccrualsType Type { get; set; }
    }
}
