using System.Collections.Generic;

namespace Lesson11.BL
{
    /// <summary>
    /// Описание для /Организации/
    /// </summary>
    public interface ICompany: IEntity
    {
        /// <summary>
        /// Наименование организации
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Список департаментов
        /// </summary>
        public IEnumerable<IDepartment> Departments { get; set; }
    }
}
