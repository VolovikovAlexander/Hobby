using Lesson11.BL.Enums;
using System;

namespace Lesson11.BL
{
    /// <summary>
    /// Интерфейс для описания документа
    /// </summary>
    public interface IDocument: IEntity
    {
        /// <summary>
        /// Серия
        /// </summary>
        public string Serial { get; set; }

        /// <summary>
        /// Номер
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Коментарии
        /// </summary>
        public string Comments { get; set; }

        /// <summary>
        /// Тип документа
        /// </summary>
        public DocumentType Type { get; set; }

        /// <summary>
        /// Дата выдачи
        /// </summary>
        public DateTime Period { get; set; }
    }
}
