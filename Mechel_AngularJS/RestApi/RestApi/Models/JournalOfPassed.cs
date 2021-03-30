using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi.Models
{
    using FluentNHibernate.Mapping;

    /// <summary>
    /// Сущность: Обработанный журнал
    /// </summary>
    public class JournalOfPassed: IBaseEntity
    {
        public virtual Int64 ID { get; set; }

        public virtual Int64 SourceID {get;set;}

        /// <summary>
        /// Дата, когда фактически услуга должна стартовать - автомат назначает
        /// </summary>
        public virtual string FactPeriod { get; set; }
       
    }

    public class JournalOfPassedMap: ClassMap<JournalOfPassed>
    {
        public JournalOfPassedMap()
        {
            Table("tblJournalOfPassed");
            Id(x => x.ID, "ID");

            Map(x => x.FactPeriod, "FactPeriod");
            Map(x => x.SourceID, "SourceID");
        }
    }
}
