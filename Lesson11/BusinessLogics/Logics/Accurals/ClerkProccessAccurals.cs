using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson11.BL
{
    /// <summary>
    /// Процесс расчета заработной платы для обычного сотрудника. Начисляется за каждый рабочий день по 12$
    /// </summary>
    public class ClerkProccessAccurals: AbstractProccessAccurals
    {
        /// <summary>
        /// Процесс расчета заработной платы
        /// </summary>
        /// <param name="emploee"> Сотрудник </param>
        /// <param name="period"> Период </param>
        /// <returns></returns>
        public override IEnumerable<IAccruals> Proccess(IEmploee emploee, DateTime period)
        {
            if (_context is null)
                throw new InvalidOperationException("Для расчета заработной платы необходимо передать контекст!");

            if (emploee is null)
                throw new ArgumentNullException("Некорректно переданы параметры!", nameof(emploee));

            var startPeriod = new DateTime(period.Year, period.Month, 1);
            var stopPeriod = new DateTime(period.Year, period.Month + 1, 1).AddDays(-1);
            var result = new List<IAccruals>();

            // Проверка. Есть ли вообще такой сотрудник?
            var tryEmploee = _context.Departments
                                .SelectMany(x => x.Emploees)
                                .Where(x => x.Id == emploee.Id)
                                .FirstOrDefault();

            if (tryEmploee is null)
                throw new InvalidOperationException($"Сотрудник {emploee.ToString()} не работает в текущей организации!");

            // Проверка. Есть ли начисления за указанный период?
            var oldCosts = tryEmploee.Tariffs.Keys.Where(x => x >= startPeriod && x <= stopPeriod);
            if (oldCosts.Any())
                foreach (var item in oldCosts)
                    tryEmploee.Tariffs.Remove(item);

            // Начисление за каждый рабочий день по 12$
            var currentPeriod = startPeriod;
            while(currentPeriod < stopPeriod)
            {
                var dayOfWeek = currentPeriod.DayOfWeek;
                if (!(dayOfWeek == DayOfWeek.Saturday || dayOfWeek == DayOfWeek.Sunday))
                {
                    var template = base.Proccess(emploee, period).First();
                    template.Type = Enums.AccrualsType.Day;
                    template.Cost = 12;
                    template.Period = currentPeriod;

                    result.Add(template);
                }

                currentPeriod = currentPeriod.AddDays(1);
            }

            return result;
        }
    }
}
