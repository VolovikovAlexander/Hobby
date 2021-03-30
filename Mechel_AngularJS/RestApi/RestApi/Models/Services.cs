using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi.Models
{
    using FluentNHibernate.Mapping;

    /// <summary>
    /// Сущность - услуги
    /// </summary>
    public class Services: IBaseEntity
    {
        public virtual Int64 ID { get; set; }
        public virtual string Description { get; set; }
        public virtual Int64 TimeLimit { get; set; }
    }

    /// <summary>
    /// Маппинг на таблицу refServices
    /// </summary>
    public class ServicesMap: ClassMap<Services>
    {
        public ServicesMap()
        {
            Table("refServices");
            Id(x => x.ID, "ID").GeneratedBy.Identity();

            Map(x => x.Description, "Description");
            Map(x => x.TimeLimit, "TimeLimit");
        }
    }
}
