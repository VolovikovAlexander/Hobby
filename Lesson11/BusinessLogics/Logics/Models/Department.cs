using System;
using System.Collections.Generic;

namespace Lesson11.BL
{
    /// <summary>
    /// Реализация интерфейса <see cref="IDepartment"/>
    /// </summary>
    public class Department : AbstractEntity, IDepartment
    {
        protected IEmploee _boss;

        #region IDepartment
        public string Description { get; set; }
        public IEmploee Boss
        {
            get => _boss; set
            {
                if (value is null)
                    throw new ArgumentNullException("Некорректно переданы параметры!", nameof(Boss));
                _boss = value;
            }
        }
        public IEnumerable<IEmploee> Emploees { get; set;}

        #endregion
    }
}
