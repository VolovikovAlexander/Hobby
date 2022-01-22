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
                // .SelectMany(x => x.Items, (x, y) => 
                .Select(x => 
                 new InnerHostStructure() { Host = x.Host, UniqueCode = x.UniqueCode });

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

            // Формируем таблицу со всеми вариантами
            var sourceTable = items.Where(x => x.UniqueCode != source.UniqueCode).ToDataTable();

            // Формируем динамический отбор по хосту
            sourceTable.DefaultView.RowFilter = " Host like " + string.Join(" OR Host like ", source.FindStataments.ToArray());

            // Получаем результат
            var table = sourceTable.DefaultView.ToTable();
            var result = (from s in table.AsEnumerable()
                         select new
                         {
                             parentCode = Convert.ToInt32(s["UniqueCode"]),
                             Length = s["Host"].ToString().Length
                         })
                         // Берем только более короткие хосты.
                         .Where(x => x.Length < source.Host.Length)
                         // Сортируем.
                         .OrderBy(x => x.Length);

            // Получаем самый первый элемент - это и есть самый короткий хост.
            return items.FirstOrDefault(x => x.UniqueCode == (result.FirstOrDefault()?.parentCode ?? -1));
        }
    }
}
