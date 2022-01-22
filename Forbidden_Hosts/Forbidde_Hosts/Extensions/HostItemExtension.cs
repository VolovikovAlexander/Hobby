using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Forbidden_Hosts
{
    internal class InnerHostStructure
    {
        public int UniqueCode { get; set; }
        public string Host { get; set; }
    }

    /// <summary>
    /// Набор расширений для формирования BL
    /// </summary>
    public static class HostItemExtension
    {
        /// <summary>
        /// Сконвертировать список в таблицу <see cref="DataTable"/>
        /// </summary>
        /// <param name="source"> Исходный список </param>
        /// <returns></returns>
        private static DataTable ToDataTable(this IEnumerable<HostItem> source)
        {
            var table = new DataTable();
            var tableData = source
                 .SelectMany(x => x.Items, (x, y) => 
                 new InnerHostStructure() { Host = y, UniqueCode = x.UniqueCode });

            var columns = typeof(InnerHostStructure)
                    .GetProperties()
                    .Select(x => x.Name);
            table.Columns.AddRange(columns.Select(x => new DataColumn() { ColumnName = x }).ToArray());
            
            foreach(var item in tableData)
                table.Rows.Add(
                    columns.Select(x => item.GetType().GetProperty(x).GetValue(item)).ToArray());

            return table;
        }

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

            // Формируем две таблицы. 
            // Первая таблица со всеми вариантами 
            // Вторая таблица на основе текущего элемента
            var sourceTable = items.Where(x => x.UniqueCode != source.UniqueCode).ToDataTable();
            var destTable = ((new[] { source }).AsEnumerable()).ToDataTable();

            // Теперь объединяю таблицы по хосту и определяю все связи для построения связанности.
            var rows = (from s in sourceTable.AsEnumerable()
                              join p in destTable.AsEnumerable() on s["Host"] equals p["Host"]
                              group s by s["UniqueCode"] into sourceGroup
                              select new
                              {
                                  parentCode = Convert.ToInt32( sourceGroup.Key),
                                  Quantity = sourceGroup.Count()
                              })
                              // Берем только более короткие хостя.
                              .Where(x => x.Quantity < source.Items.Count())
                              // Сортируем.
                              .OrderBy(x => x.Quantity);

            // Получаем самый первый элемент - это и есть самый короткий хост.
            return items.FirstOrDefault(x => x.UniqueCode == (rows.FirstOrDefault()?.parentCode ?? -1));
        }
    }
}
