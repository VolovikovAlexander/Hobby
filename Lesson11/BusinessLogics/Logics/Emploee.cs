using Lesson11.BL.Enums;
using System;
using System.Collections.Generic;

namespace Lesson11.BL
{
    /// <summary>
    /// Реализация интерфейса <see cref="IEmploee"/>
    /// </summary>
    public class Emploee : AbstractEntity, IEmploee
    {
        protected IDictionary<DateTime, IAccuralsTariff> _tariffs = new Dictionary<DateTime, IAccuralsTariff>();
        private string _firstName = "";

        #region IEmploee
        public string FirstName { get => _firstName; set => 
                    _firstName = (string.IsNullOrEmpty(value) ? throw new ArgumentException("Некорректно переданы параметры!",nameof(FirstName)) : value) ; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string PhoneNumber { get; set; }
        public IEnumerable<IDocument> Documents { get; set; }
        public EmploeeType Type { get; set; }
        public DateTime BirthDay { get; set; }

        /// <summary>
        /// Тарифы для начисления заработной платы
        /// </summary>
        public IDictionary<DateTime, IAccuralsTariff> Tariffs { get => _tariffs; set => _tariffs = value; }

        #endregion
    }
}
