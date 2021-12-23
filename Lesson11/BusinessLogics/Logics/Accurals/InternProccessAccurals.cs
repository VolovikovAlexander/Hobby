using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson11.BL
{
    /// <summary>
    /// Процесс начисления для интерна. Разовая сумма - 500$
    /// </summary>
    public class InternProccessAccurals: AbstractProccessAccurals
    {
        /// <summary>
        /// Расчет заработной платы
        /// </summary>
        /// <param name="emploee"> Сотрудник </param>
        /// <param name="period"> Период </param>
        /// <returns></returns>
        public override IEnumerable<IAccruals> Proccess(IEmploee emploee, DateTime period)
        {
            if (emploee is null)
                throw new ArgumentNullException("Некорректно переданы параметры!", nameof(emploee));

            var result = base.Proccess(emploee, period).First();
            result.Type = Enums.AccrualsType.Month;
            result.Cost = 500;

            return new[] { result };
        }
    }
}
