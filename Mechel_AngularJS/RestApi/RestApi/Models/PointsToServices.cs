using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi.Models
{
    using FluentNHibernate.Mapping;

    /// <summary>
    /// Сущность - связка точки оказания услуг со списком услуг
    /// </summary>
    public class PointsToServices: IBaseEntity
    {
        public virtual Int64 ID { get; set; }
        /// <summary>
        /// Код точки оказания услуг
        /// </summary>
        public virtual Int64 PointID { get; set; }

        /// <summary>
        /// Код услуги
        /// </summary>
        public virtual Int64 ServiceID { get; set; }
    }

    /// <summary>
    /// Маппинг на таблицу rblPointsToServicesMap
    /// </summary>
    public class PointsToServicesMap: ClassMap<PointsToServices>
    {
        public PointsToServicesMap()
        {
            Table("tblPointsToServices");
            Id(x => x.ID, "ID").GeneratedBy.Identity();

            Map(x => x.PointID, "PointID");
            Map(x => x.ServiceID, "ServiceID");
        }
    }
}
