using Lesson11.BL.Enums;
using System;
using System.Collections.Generic;

namespace Lesson11.BL
{
    /// <summary>
    /// Абстрактный класс для формирования насичления по интефейсу <see cref="IProccessAccurals"/>
    /// </summary>
    public abstract class AbstractProccessAccurals: AbstractEntity, IProccessAccurals
    {
        protected IEmploee _emploee;
        protected DateTime _period;
        protected ICompany _context;

        #region constructors

        public AbstractProccessAccurals() { }

        #endregion

        #region IProccessAccurals

        public double MinimalCost { get; set; }

        public virtual EmploeeType Type => EmploeeType.None;

        /// <summary>
        /// Провести начисление. 
        /// </summary>
        /// <param name="emploee"> Сотрудник </param>
        /// <param name="period"> Период начисления </param>
        /// <returns>Возвращает структуру <see cref="IAccruals"/> с заполненными базовыми реквизитами</returns>
        public virtual IEnumerable<IAccruals> Proccess(ICompany context, IEmploee emploee, DateTime period)
        {
            _context = context;

            if (emploee is null)
                throw new ArgumentNullException("Некорректно переданы параметры!", nameof(emploee));

            if (_context is null)
                throw new ArgumentException("Для расчета заработной платы необходимо передать контекст!", nameof(_context));

            _emploee = emploee;
            _period = period;

            return new[] { new Accurals() { Emploee = emploee, EmploeeType = emploee.Type, Period = period } };
        }

        #endregion

        /// <summary>
        /// Рассчитать начало и окончания месяца
        /// </summary>
        /// <param name="period"></param>
        /// <returns></returns>
        protected Tuple<DateTime, DateTime> GetPeriod(DateTime period)
        {
            var startPeriod = new DateTime(period.Year, period.Month, 1);
            var stopPeriod = startPeriod.AddMonths(1).AddDays(-1);
            return new Tuple<DateTime, DateTime>(startPeriod, stopPeriod);
        }
    }
}
