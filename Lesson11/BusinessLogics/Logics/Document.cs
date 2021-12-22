using Lesson11.BL.Enums;
using System;

namespace Lesson11.BL
{
    /// <summary>
    /// Реализация интерфейса <see cref="IDocument"/>
    /// </summary>
    public class Document : AbstractEntity, IDocument
    {
        #region IDocument
        public string Serial { get; set; }
        public string Number { get; set; }
        public string Comments { get; set; }
        public DocumentType Type { get; set; }
        public DateTime Period { get; set; }

        #endregion
    }
}
