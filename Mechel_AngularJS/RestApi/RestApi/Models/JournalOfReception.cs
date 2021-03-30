using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi.Models
{
    using FluentNHibernate.Mapping;

    /// <summary>
    /// Сущность: Журнал регистрации заявок посетителей на то время, которое им удобно получить услугу
    /// </summary>
    public class JournalOfReception : IBaseEntity
    {
        public virtual Int64 ID { get; set; }

        public virtual Int64 ServiceID { get; set; }

        public virtual Int64 PointID { get; set; }

        /// <summary>
        /// Представление даты и времени в базе данных
        /// </summary>
        public virtual string StartPeriod { get; set; }
       
    }

    /// <summary>
    /// Маппинг к таблице tblJournalOfReception
    /// </summary>
    public class JournalOfReceptionMap: ClassMap<JournalOfReception>
    {
        public JournalOfReceptionMap()
        {
            Table("tblJournalOfReception");
            Id(x => x.ID, "ID");

            Map(x => x.ServiceID, "ServiceID");
            Map(x => x.PointID, "PointID");
            Map(x => x.StartPeriod, "StartPeriod");
        }
    }
}
