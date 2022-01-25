using System;
using System.Collections.Generic;
using System.Linq;

namespace Forbidden_Hosts
{
    public static class StringArrayExtension
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
                // TODO: Позже, переделать на другой тип уникального номера. На Hash!
                //.AsParallel()
                .Select(x => x.ToHost());

        /// <summary>
        /// Расширение для формирования списка хостов для поиска в общей таблице
        /// </summary>
        /// <param name="source"> Исходная строка с наименованием хоста </param>
        /// <returns></returns>
        public static IEnumerable<string> BuildFindStataments(this string source)
        {
            if(string .IsNullOrEmpty(source))
                return Enumerable.Empty<string>();

            var items = source.Split('.').AsEnumerable();
            if (!items.Any())
                return Enumerable.Empty<string>();
    
            // Сортируем в обратном порядке
            var sorted = items
                    .Select((x, index) => new { host = x, position = index })
                    .OrderByDescending(x => x.position)
                    .Select(x => x.host);

            // Формируем список вариантов.
            var findHost = "";
            var findItems = new List<string>();
            foreach (var item in sorted)
            {
                findHost = string.Format(".{0}{1}", item, findHost);
                findItems.Add(string.Format("'%{0}%'", findHost));
            }

            return findItems.AsEnumerable();
        }
    }
}
