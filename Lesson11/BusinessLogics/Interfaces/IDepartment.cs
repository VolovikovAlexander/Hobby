using System.Collections.Generic;

namespace Lesson11.BL
{
    /// <summary>
    /// Интерфейс для сущности /Департамент/
    /// </summary>
    public interface IDepartment: IEntity
    {
        /// <summary>
        /// Наименование
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Руководитель
        /// </summary>
        public IEmploee Boss { get; set; }

        /// <summary>
        /// Список сотрудников
        /// </summary>
        public IEnumerable<IEmploee> Emploees { get; set; }
    }
}
