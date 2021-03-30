using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi.Dto
{
    using RestApi.Models;

    /// <summary>
    /// Информация из обработанного журнала
    /// </summary>
    public class JournalOfPassedDto
    {
        private string _serviceName = "";
        private string _pointName = "";
        private Int64 _serviceId = 0;
        private Int64 _pointId = 0;
        private DateTime _startPeriod;
        private DateTime _stopPeriod;

        #region ref
        public Int64 ID { get; set; }

        public Int64 ServiceID { get => _serviceId; 
            set 
            {
                _serviceId = value;
                if (value > 0)
                {
                    using var session = NHibernateHelper<Services>.OpenSession();
                    var item = session.Get<Services>(ServiceID);

                    _serviceName = item?.Description.Trim();
                }
                else
                    throw new Exception("Не корректно передан код услуги!");
            }
        }

        public string ServiceName { get => _serviceName; }

        public Int64 PointID
        {
            get => _pointId;
            set
            {
                _pointId = value;
                if (value > 0)
                {
                    using var session = NHibernateHelper<ReceptionPoints>.OpenSession();
                    var item = session.Get<ReceptionPoints>(PointID);

                    _pointName = item?.Description.Trim();
                }
                else
                    throw new Exception("Не корректно передан код пункта приема!");
            }
        }


        public string PointName { get => _pointName; }

        /// <summary>
        /// Начало выполнения
        /// </summary>
        public DateTime StartPeriod { get => _startPeriod; 
            set 
            {
                _startPeriod = value;
                if (_serviceId != 0)
                {
                    using var session = NHibernateHelper<Services>.OpenSession();
                    var item = session.Get<Services>(_serviceId);

                    // Автоматически дату завершения получаем
                    _stopPeriod = _startPeriod.AddMinutes(item.TimeLimit); 
                }
            } 
        }

        /// <summary>
        /// Окончание выполнения
        /// </summary>
        public DateTime StopPeriod { get => _stopPeriod;  }

        #endregion

        /// <summary>
        /// (ВИД проблемы (услуги) + текущий номер в очереди на услугу)
        /// </summary>
        public string Ticket
        {
            get
            {
                return string.Format("{0}_{1}", _serviceName.Replace(" ", "_"), ID);
            }
        }

        /// <summary>
        /// Преобразовать исходный список в Dto
        /// </summary>
        /// <param name="sourceJournal"></param>
        /// <returns></returns>
        public static IEnumerable<JournalOfPassedDto> ConvertTo(IEnumerable<JournalOfPassed> sourceItems)
        {
            if (sourceItems == null)
                return null;

            // 1. Получим исходный журнал и все связанные записи
            var factIds = sourceItems.Select(x => x.SourceID).ToList();
            using var sessionJournal = NHibernateHelper<JournalOfReception>.OpenSession();
            var sourceList = sessionJournal.Query<JournalOfReception>()
                .Where(x => factIds.Contains(x.ID));

            using var sessionServices = NHibernateHelper<Services>.OpenSession();
            var servicesList = sessionServices.Query<Services>();

            // 2. Объединим записи
            var list = from s in sourceItems
                       join p in sourceList on s.SourceID equals p.ID
                        select new { FactJournal = s, RegisterJournal = p};

            // 3. Формируем dto
            var result = list.Select(x => new JournalOfPassedDto()
            {
                ID = x.FactJournal.ID,
                ServiceID = x.RegisterJournal.ServiceID,
                PointID = x.RegisterJournal.PointID,

                // TODO: Потом сделать безопасное преобразование!
                StartPeriod = Convert.ToDateTime( x.FactJournal.FactPeriod),
            });

            return result.ToList();
        }

        /// <summary>
        /// Преобразовать в JournalOfPassedDto
        /// </summary>
        /// <param name="sourceItems"></param>
        /// <returns></returns>
        public static IEnumerable<JournalOfPassedDto> ConvertTo(IEnumerable<PassedRecordsDto> sourceItems)
        {
            if (sourceItems == null)
                return null;

            return sourceItems.Select(x => new JournalOfPassedDto()
            {
                ID = x.ID,
                ServiceID = x.ServiceID,
                PointID = x.PointID,
                StartPeriod = Convert.ToDateTime(x.FactPeriod),
            });
        }
    }
}
