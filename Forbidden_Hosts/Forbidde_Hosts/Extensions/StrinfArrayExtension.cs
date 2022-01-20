using System;
using System.Collections.Generic;
using System.Linq;

namespace Forbidden_Hosts
{
    public static class StrinfArrayExtension
    {
        private static Random _rnd = new Random();

        /// <summary>
        /// Расширение для формирования структуры <see cref="HostItem"/> из строки.
        /// </summary>
        /// <param name="source"> Строка </param>
        /// <returns></returns>
        public static HostItem ToHost(this string source)
            => new HostItem() { UniqueCode = _rnd.Next(99999999), Host = source };

        /// <summary>
        /// Расширение для формирования списка структур <see cref="HostItem"/> из массива строк.
        /// </summary>
        /// <param name="source"> Массив строк. </param>
        /// <returns></returns>
        public static IEnumerable<HostItem> ToHosts(this string[] source)
            => source
                .AsParallel()
                .Select(x => x.ToHost());
    }
}
