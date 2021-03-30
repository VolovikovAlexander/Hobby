using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi.Dto
{
    using RestApi.Models;

    /// <summary>
    /// Информация о регистрации записи в журнале
    /// </summary>
    public class ItemOfRegistartionDto
    {
        #region ref

        public DateTime Period { get; set; }

        public Int64 PointID {get;set;}

        public Int64 ServiceID { get; set; }

        #endregion

        /// <summary>
        /// Преобразование один к одному
        /// </summary>
        /// <param name="sourceItem"></param>
        /// <returns></returns>
        public static JournalOfReception ConvertTo(ItemOfRegistartionDto sourceItem)
        {
            return new JournalOfReception()
            {
                PointID = sourceItem.PointID,
                ServiceID = sourceItem.ServiceID,
                StartPeriod = sourceItem.Period.ToString("yyyy-MM-dd HH:mm")
            };
        }
    }
}
