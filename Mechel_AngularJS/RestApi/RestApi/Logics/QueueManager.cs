using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi.Logics
{
    using RestApi.Models;
    using RestApi.Dto;
    using NHibernate.Transform;

    /// <summary>
    /// Реализация бизнес логики для электронной очереди
    /// </summary>
    public static class QueueManager
    {
        #region private

        /// <summary>
        /// Получить список новых записей, которые еще не обрабатывались
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<JournalOfReception> GetNewRecords()
        {
            // Логика поиска новых записей и откидывание дублей 
            // реализовано в представлении vw_GetNewRecords
            using var session = NHibernateHelper<JournalOfReception>.OpenSession();
            var sql = "Select * from vw_GetNewRecords";
            var query = session.CreateSQLQuery(sql);

            var result = query
                    .SetResultTransformer(new AliasToBeanResultTransformer(typeof(JournalOfReception)))
                    .List<JournalOfReception>();

            return result.AsEnumerable();
        }

        /// <summary>
        /// Получить все записи из фактического журнала
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<PassedRecordsDto> GetPassedRecords()
        {
            using var session = NHibernateHelper<JournalOfReception>.OpenSession();
            var sql = "Select * from vw_GetPassedRecords";
            var query = session.CreateSQLQuery(sql);

            var result = query
                    .SetResultTransformer(new AliasToBeanResultTransformer(typeof(PassedRecordsDto)))
                    .List<PassedRecordsDto>();

            return result.AsEnumerable();
        }

        /// <summary>
        /// Записать в журнал фактов
        /// </summary>
        /// <param name="items"></param>
        private static void SaveItems(IEnumerable<JournalOfPassed> items)
        {
            if (!items.Any()) return;

            using (var session = NHibernateHelper<JournalOfPassed>.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        items.ToList().ForEach(x =>
                        {
                            session.Save(x);
                        });

                        transaction.Commit();
                        session.Flush();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                    }
                }
            }
        }

        #endregion

        /// <summary>
        /// Перебросить записи из журнала регистрации в журнал фактов
        /// с определением верной записи
        /// </summary>
        public static void BuildProcess(
                    IEnumerable<JournalOfReception> sourceRecords = null
            )
        {
            var newRecords = sourceRecords != null ? sourceRecords : GetNewRecords();
            var factRecords = GetPassedRecords();

            if (newRecords .Any())
            {
                var journal = JournalOfPassedDto.ConvertTo(factRecords);
                var destPeriods = journal.Select(x => new { StartPeriod = x.StartPeriod, StopPeriod = x.StopPeriod });
                var sourcePeriods = newRecords.Select(x => Convert.ToDateTime(x.StartPeriod));

                var startPeriod = !destPeriods.Any() ? sourcePeriods.Min() : destPeriods.Min(y => y.StartPeriod);
                var stopPeriod = !destPeriods.Any() ? sourcePeriods.Max() : destPeriods.Max(y => y.StopPeriod);

                // Проверяем по периодам. Разобъем список на части.
                // Одна часть будет вставлена в журнал фактов в период "Начало" и "Окончание"
                // Другая часть будет сдвинута поле даты "Окончание"

                using var sessionServices = NHibernateHelper<Services>.OpenSession();
                var services = sessionServices.Query<Services>();

                // Список с периодаи из новых записей
                var sourceList = (from s in newRecords
                            join p in services on s.ServiceID equals p.ID
                            select new { Records = s, Service = p })
                           .Select(x => new
                           {
                               StartPeriod = Convert.ToDateTime(x.Records.StartPeriod),
                               StopPeriod = Convert.ToDateTime(x.Records.StartPeriod)
                               .AddMinutes(x.Service.TimeLimit),
                               ID = x.Records.ID
                           }).ToList();

                // Список с периодами из имеющихся записей
                var destList = factRecords
                    .Select(x => new
                    {
                        StartPeriod = Convert.ToDateTime(x.FactPeriod),
                        StopPeriod = Convert.ToDateTime(x.FactPeriod)
                                    .AddMinutes(x.TimeLimit),
                        ID = x.SourceID
                    });


                List<JournalOfPassed> insertRecords = new List<JournalOfPassed>();
                sourceList.ForEach(x =>
                {
                    if (destList.Any(y => y.StartPeriod >= x.StartPeriod && y.StartPeriod <= x.StartPeriod))
                        insertRecords.Add(new JournalOfPassed()
                        { SourceID = x.ID, FactPeriod = x.StartPeriod.ToString("yyyy-MM-dd HH:mm") });

                });

                // Оставшуюся часть добавляем после даты окончания
                var leftRecords = sourceList
                    .Where(x => !insertRecords.Any(y => y.SourceID == x.ID))
                    .Select(x => new JournalOfPassed()
                    {
                        SourceID = x.ID,
                        FactPeriod = stopPeriod.ToString("yyyy-MM-dd HH:mm")
                    });

                // Сохраняю все в базе данных
                SaveItems(insertRecords.ToList());
                SaveItems(leftRecords.ToList());
            }
        }
    }
}
