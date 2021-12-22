using Lesson11.BL.Enums;
using System;

namespace Lesson11.BL
{
    /// <summary>
    /// Реализация структуры для начисления заработной платы.
    /// </summary>
    public class Accurals : AbstractEntity, IAccruals
    {
        private IEmploee _emploee;

        #region IAccruals
        public DateTime Period { get; set; }
        public IEmploee Emploee
        {
            get => _emploee;
            set
            {
                if (value is null)
                    new ArgumentNullException("Некорректно переданы параметры!", nameof(Emploee));
                _emploee = value;
            }
        }
        public double Cost { get; set; }
        public EmploeeType EmploeeType { get; set; }
        public AccrualsType Type { get; set; }

        #endregion
    }
}
