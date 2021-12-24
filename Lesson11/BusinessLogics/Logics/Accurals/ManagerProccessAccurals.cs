using Lesson11.BL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson11.BL
{
    /// <summary>
    /// Поцесс начисления заработной платы для менеджера. 15% от суммы начисления всех сотрудников для всех отделов где менеджер является руководителем.
    /// </summary>
    public class ManagerProccessAccurals: AbstractProccessAccurals
    {
        public ManagerProccessAccurals() { MinimalCost = AccuralsHelper.Manager; }

        /// <summary>
        /// Процесс расчета заработной платы.
        /// </summary>
        /// <param name="emploee"></param>
        /// <param name="period"></param>
        /// <returns></returns>
        public override IEnumerable<IAccruals> Proccess(ICompany context, IEmploee emploee, DateTime period)
        {
            var periods = AccuralsHelper.GetPeriod(period);
            var startPeriod = periods.Item1;
            var stopPeriod = periods.Item2;
            var result = base.Proccess(context, emploee, period).First();

            if (_context is null)
                throw new InvalidOperationException("Для расчета заработной платы необходимо передать контекст!");

            if (emploee is null)
                throw new ArgumentNullException("Некорректно переданы параметры!", nameof(emploee));

            result.Type = Enums.AccrualsType.Month;

            // 1 Период устанавливаем всегда : конец месяца
            result.Period = stopPeriod;

            // 2. Ставка менеджера: процент от расчета по всем сотрудникам- 15%
            if (!_context.Departments.Where(x => x.Boss == _emploee).Any())
                throw new InvalidOperationException($"Сотрудник: {_emploee.ToString()} не содержится не в одном подразделении!");

            var allTariffs = _context.Departments
                                .Where(x => x.Boss == _emploee)
                                .SelectMany(x => x.Emploees)
                                .SelectMany(x => x.Tariffs);

            if (!allTariffs.Where(x => x.Key >= startPeriod && x.Key <= stopPeriod).Any())
                // Если нет сотрудников или нет начислений, то минимальную сумму платим.
                result.Cost = MinimalCost;
            else
            {
                // Рассчитываем вознаграждение
                var allCosts = allTariffs.Where(x => x.Key >= startPeriod && x.Key <= stopPeriod)
                            .Select(x => x.Value);
                result.Cost = (allCosts.Sum(x => x.Cost) * 100) / 15;
                result.Cost = result.Cost < MinimalCost ? MinimalCost : result.Cost;
            }

            return new[] { result };
        }

        /// <summary>
        /// Тип сотрудника
        /// </summary>
        public override EmploeeType Type => EmploeeType.Manager;
    }
}
