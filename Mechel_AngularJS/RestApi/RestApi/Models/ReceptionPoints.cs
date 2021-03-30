using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi.Models
{
    using FluentNHibernate.Mapping;

    /// <summary>
    /// Сущность - точки приема услуг
    /// </summary>
    public class ReceptionPoints:IBaseEntity
    {
        public virtual Int64 ID { get; set; }
        public virtual string Description { get; set; }

    }

    /// <summary>
    /// Маппинг на таблицу refReceptionPoints
    /// </summary>
    public class ReceptionPointsMap: ClassMap<ReceptionPoints>
    {
        public ReceptionPointsMap()
        {
            Table("refReceptionPoints");

            Id(x => x.ID, "ID").GeneratedBy.Identity();

            Map(x => x.Description, "Description");
        }
    }
}
