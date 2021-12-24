using Lesson11.BL.Enums;
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
        public ClerkProccessAccurals() { MinimalCost = AccuralsHelper.Clerk; }


        /// <summary>
        /// Процесс расчета заработной платы
        /// </summary>
        /// <param name="emploee"> Сотрудник </param>
        /// <param name="period"> Период </param>
        /// <returns></returns>
        public override IEnumerable<IAccruals> Proccess(ICompany context, IEmploee emploee, DateTime period)
        {
            _context = context;

            if (_context is null)
                throw new InvalidOperationException("Для расчета заработной платы необходимо передать контекст!");

            if (emploee is null)
                throw new ArgumentNullException("Некорректно переданы параметры!", nameof(emploee));

            var periods = AccuralsHelper.GetPeriod(period);
            var startPeriod = periods.Item1;
            var stopPeriod = periods.Item2;
            var result = new List<IAccruals>();

            // Проверка. Есть ли начисления за указанный период?
            var oldCosts = emploee.Tariffs.Keys.Where(x => x >= startPeriod && x <= stopPeriod);
            if (oldCosts.Any())
                foreach (var item in oldCosts)
                    emploee.Tariffs.Remove(item);

            // Начисление за каждый рабочий день по 12$
            var list = AccuralsHelper.GetWorkPeriod(startPeriod, stopPeriod);
            list.ToList().ForEach(x =>
            {
               var template = base.Proccess(context, emploee, period).First();
               template.Type = Enums.AccrualsType.Day;
               template.Cost = MinimalCost;
               template.Period = x;
               result.Add(template);

            });

            return result;
        }

        /// <summary>
        /// Тип сотрудника
        /// </summary>
        public override EmploeeType Type => EmploeeType.Clerk;
    }
}
