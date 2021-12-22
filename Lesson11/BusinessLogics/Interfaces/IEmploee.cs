using Lesson11.BL.Enums;
using System;
using System.Collections.Generic;

namespace Lesson11.BL
{
    /// <summary>
    /// Интерфейс ддля описания /Сотрудника/
    /// </summary>
    public interface IEmploee : IEntity
    {
        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }

        
        /// <summary>
        /// Отчество
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Телефон
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Список документов
        /// </summary>
        public IEnumerable<IDocument> Documents { get; set; }

        /// <summary>
        /// Тип сотрудника
        /// </summary>
        public EmploeeType Type { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime BirthDay { get; set; }

        /// <summary>
        /// Список тарифов для начисления заработной платы
        /// </summary>
        public IDictionary<DateTime , IAccuralsTariff> Tariffs { get; set; }
    }
}
