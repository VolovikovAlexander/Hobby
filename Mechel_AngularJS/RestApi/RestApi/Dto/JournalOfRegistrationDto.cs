using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi.Dto
{
    using RestApi.Models;

    /// <summary>
    /// Журнал регистрации
    /// </summary>
    public class JournalOfRegistrationDto
    {
        #region "ref"
        public Int64 ID { get; set; }

        public Int64 ServiceID { get; set; }

        public Int64 PointID { get; set; }

        public DateTime Period { get; set; }

        /// <summary>
        /// Получить ссылку на услуги
        /// TODO: Для большого объема такой способ не корректен!
        /// </summary>
        public Services Service 
        {
            get
            {
                var session = NHibernateHelper<Services>.OpenSession();
                return session.Get<Services>(ServiceID);
            }
            private set
            {
                if (value != null)
                    ServiceID = value.ID;
                else
                    ServiceID = 0;
            }
        }

        /// <summary>
        /// Получить ссылку на окно оказания услуг
        /// TODO: Для больших данных такой способ не подойдет
        /// </summary>
        public ReceptionPoints Point
        {
            get
            {
                var session = NHibernateHelper<ReceptionPoints>.OpenSession();
                return session.Get<ReceptionPoints>(PointID);
            }
            private set
            {
                if (value != null)
                    PointID = value.ID;
                else
                    PointID = 0;
            }
        }

        #endregion

        /// <summary>
        /// Сконвертировать в dto
        /// </summary>
        /// <param name="sourceItems"></param>
        /// <returns></returns>
        public static IEnumerable<JournalOfRegistrationDto> ConvertTo(IEnumerable<JournalOfReception> sourceItems)
        {
            if (sourceItems == null)
                return null;

            var result = sourceItems
                .Select(x => new JournalOfRegistrationDto()
                {
                    ID = x.ID,
                    PointID = x.PointID,
                    ServiceID = x.ServiceID,
                    // TODO: Тут в дальнейшем организовать безопасную конвертацию!
                    Period = Convert.ToDateTime(x.StartPeriod)
                });

            return result.ToList();
        }

    }
}
