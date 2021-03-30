using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using RestApi.Models;
using RestApi.Dto;
using Microsoft.AspNetCore.Cors;
using System.Threading;

namespace RestApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        public HomeController()
        {
        }

        /// <summary>
        /// Получить список сервисов для точки оказания продаж
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetServices")]
        [EnableCors()]
        public IEnumerable<Services> GetServices(int? pointID)
        {
            using var firstSession = NHibernateHelper<PointsToServices>.OpenSession();
            var list = firstSession.Query<PointsToServices>();

            if(pointID != null)
                list = list
                    .Where(x => x.PointID == (pointID ?? 0));

            var serviceIds = list.Select(x => x.ServiceID).ToList();
            using var secondSession = NHibernateHelper<Services>.OpenSession();

            // Получить все услуги, которые связаны
            var result = secondSession.Query<Services>()
                .ToList()
                .Where(x => serviceIds.Any(y => y == x.ID))
                .Distinct()
                .OrderBy(x => x.Description);

            return result.ToList();
        }

        /// <summary>
        /// Получить список всех пунктов оказания услуг
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetReceptionPoints")]
        [EnableCors()]
        public IEnumerable<ReceptionPoints> GetReceptionPoints()
        {
            using var session = NHibernateHelper<ReceptionPoints>.OpenSession();
            var items = session.Query<ReceptionPoints>().ToList();
            return items;
        }

        /// <summary>
        /// Получить журнал регистрации для точки продаж
        /// </summary>
        /// <param name="pointID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetJournalOfRegistration")]
        [EnableCors()]
        public IEnumerable<JournalOfRegistrationDto> GetJournalOfRegistration(int? pointID)
        {
            using var session = NHibernateHelper<JournalOfReception>.OpenSession();
            var list = session.Query<JournalOfReception>();

            if (pointID != null)
                list = list.Where(x => x.PointID == (pointID ?? 0));

            var resut = JournalOfRegistrationDto.ConvertTo(list.ToList()).OrderByDescending(x => x.Period);
            return resut;
        }


        /// <summary>
        /// Получить журнал фактической регистрации
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetJournalOfPassed")]
        [EnableCors()]
        public IEnumerable<JournalOfPassedDto> GetJournalOfPassed()
        {
            using var session = NHibernateHelper<JournalOfPassed>.OpenSession();
            var list = session.Query<JournalOfPassed>();

            var resut = (JournalOfPassedDto.ConvertTo(list.ToList())
                .OrderBy(x => x.PointID).ThenBy(x => x.StartPeriod));
            return resut;
        }

        /// <summary>
        /// Получить обработанный журнал (с реальными цифрами, когда можно выполнить услугу)
        /// </summary>
        /// <param name="pointID"></param>
        /// <returns></returns>
        
        /// <summary>
        /// Добавить запись в журнал регистрации
        /// </summary>
        /// <param name="sourceItem"></param>
        [HttpPost]
        [Route("SendItemOfRegistration")]
        [EnableCors()]
        public void SendItemOfRegistration(ItemOfRegistartionDto sourceItem)
        {
            if(sourceItem != null)
            {
                using (var session = NHibernateHelper<JournalOfReception>.OpenSession())
                { 
                    using(var transaction = session.BeginTransaction())
                    {
                        var ItemOfJournal = ItemOfRegistartionDto.ConvertTo(sourceItem);
                        session.Save(ItemOfJournal);

                        transaction.Commit();
                        session.Flush();

                        // Запустим обработку данных
                        Task.Run(() =>
                        {
                            Logics.QueueManager.BuildProcess();
                        });
                    }
                }
            }
        }
    }
}
