using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi.Dto
{
    /// <summary>
    /// Данные из представления vw_PassedRecords
    /// </summary>
    public class PassedRecordsDto
    {
        public Int64 SourceID { get; set; }
        public Int64 PointID { get; set; }
        public string FactPeriod { get; set; }
        public Int64 TimeLimit { get; set; }
        public Int64 ID { get; set; }
        public Int64 ServiceID { get; set; }
    }

}
