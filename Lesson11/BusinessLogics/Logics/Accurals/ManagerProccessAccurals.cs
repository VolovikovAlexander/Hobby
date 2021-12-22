using System;
using System.Linq;

namespace Lesson11.BL
{
    /// <summary>
    /// Поцесс начисления заработной платы для менеджера
    /// </summary>
    public class ManagerProccessAccurals: AbstractProccessAccurals
    {
        public override IAccruals Proccess(IEmploee emploee, DateTime period)
        {
            if (_context is null)
                throw new InvalidOperationException("Для расчета заработной платы необходимо передать контекст!");

            if (emploee is null)
                throw new ArgumentNullException("Некорректно переданы параметры!", nameof(emploee));

            var startPeriod = new DateTime(period.Year, period.Month, 1);
            var stopPeriod = new DateTime(period.Year, period.Month + 1, 1).AddDays(-1);


            var result = base.Proccess(emploee, period);
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
                result.Cost = MinimalCost;
            else
            {
                var allCosts = allTariffs.Where(x => x.Key >= startPeriod && x.Key <= stopPeriod)
                            .Select(x => x.Value);
                result.Cost = allCosts.Sum(x => x.Cost);
            }

            return result;
        }
    }
}
