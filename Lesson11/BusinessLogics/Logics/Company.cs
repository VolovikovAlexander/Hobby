using System.Collections.Generic;

namespace Lesson11.BL
{
    /// <summary>
    /// Реализация интерфейса <see cref="ICompany"/>
    /// </summary>
    public class Company : AbstractEntity, ICompany
    {
        #region ICompany
        public string Description { get; set; }
        public IEnumerable<IDepartment> Departments { get; set; }

        #endregion
    }
}
