using System;
using System.Collections.Generic;
using System.Linq;

namespace Forbidden_Hosts
{
    /// <summary>
    /// Набор расширений для формирования BL
    /// </summary>
    public static class HostItemExtension
    {

        /// <summary>
        /// Получить владельца хоста
        /// </summary>
        /// <param name="source"> Исходный хост </param>
        /// <param name="items"> Список вариантов </param>
        /// <returns></returns>
        public static HostItem GetParent(this HostItem source,  IEnumerable<HostItem> items)
        {
            if (string.IsNullOrEmpty(source.Host))
                throw new ArgumentNullException("Некорректный аругумент!", nameof(source.Host));
            
            // Список не связанных хостов.
            var freeList = items.Where(x => x.Parent is null);
            if (!freeList.Any())
                return null;

            // Состав текущего хоста.
            var elements = source.Host.Split('.');
            var findElement = elements.Last();
            var findItems = freeList.Where(x => x.Host.Contains("." + findElement) 
                                            && string.Compare(x.Host, source.Host) != 0
                                            && source.Host.Length > x.Host.Length);

            // Запускаем поиск
            return source.GetParent(findItems);
        }
    }
}
