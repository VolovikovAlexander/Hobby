using System;

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

        public AbstractProccessAccurals(ICompany context)
        {
            _context = context ?? throw new ArgumentException("Некорректно переданы параметры!", nameof(Context));
        }

        #endregion

        /// <summary>
        /// Контекст для работы с данными
        /// </summary>
        public ICompany Context { set => _context = value ?? throw new ArgumentException("Некорректно переданы параметры!", nameof(Context)); }
        public double MinimalCost { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        /// <summary>
        /// Провести начисление. 
        /// </summary>
        /// <param name="emploee"> Сотрудник </param>
        /// <param name="period"> Период начисления </param>
        /// <returns>Возвращает структуру <see cref="IAccruals"/> с заполненными базовыми реквизитами</returns>
        public virtual IAccruals Proccess(IEmploee emploee, DateTime period)
        {
            if (emploee is null)
                throw new ArgumentNullException("Некорректно переданы параметры!", nameof(emploee));

            if (_context is null)
                throw new ArgumentException("Для расчета заработной платы необходимо передать контекст!", nameof(Context));

            _emploee = emploee;
            _period = period;

            return new Accurals() { Emploee = emploee, EmploeeType = emploee.Type, Period = period };
        }
    }
}
