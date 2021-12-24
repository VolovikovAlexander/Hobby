using System;
using System.Collections.Generic;

namespace Lesson11.BL
{
    public static class AccuralsHelper
    {
        /// <summary>
        /// Минимальная заработная плата для менеджера
        /// </summary>
        public readonly static double Manager = 1300.0;

        /// <summary>
        /// Минимальная заработка плата для рядового сотрудника
        /// </summary>
        public readonly static double Clerk = 12.0;

        /// <summary>
        /// Минимальная заработная плата для интерна
        /// </summary>
        public readonly static double Intern = 500.0;


        /// <summary>
        /// Получить список рабочих дней
        /// </summary>
        /// <param name="startPeriod"></param>
        /// <param name="stopPeriod"></param>
        /// <returns></returns>
        public static IEnumerable<DateTime> GetWorkPeriod(DateTime startPeriod, DateTime stopPeriod)
        {
            var currentPeriod = startPeriod;
            var result = new List<DateTime>();
            while (currentPeriod < stopPeriod)
            {
                var dayOfWeek = currentPeriod.DayOfWeek;
                if (!(dayOfWeek == DayOfWeek.Saturday || dayOfWeek == DayOfWeek.Sunday))
                    result.Add(currentPeriod);

                currentPeriod = currentPeriod.AddDays(1);
            }

            return result;
        }

        /// <summary>
        /// Рассчитать начало и окончания месяца
        /// </summary>
        /// <param name="period"></param>
        /// <returns></returns>
        public static Tuple<DateTime, DateTime> GetPeriod(DateTime period)
        {
            var startPeriod = new DateTime(period.Year, period.Month, 1);
            var stopPeriod = startPeriod.AddMonths(1).AddDays(-1);
            return new Tuple<DateTime, DateTime>(startPeriod, stopPeriod);
        }
    }
}
