using ControlPoint.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControlPoint.Models
{
    /// <summary>
    /// Интерфейс к классам с представлением данных
    /// </summary>
    public interface IViewTableModel<T> where T: ITableData
    {
        public IEnumerable<T> Items { get; set; }

        public int SomeValue { get; set; }

        public PageManager Page { get; set; }
    }

    /// <summary>
    /// Индексированное представление для таблицы 1
    /// </summary>
    public class ViewTable1Model<T>: IViewTableModel<T> where T: Table1
    {
        /// <summary>
        /// Таблица с данными
        /// </summary>
        public IEnumerable<T> Items { get; set; }

        /// <summary>
        /// Просто произвольное значение
        /// </summary>
        public int SomeValue { get; set; }

        /// <summary>
        /// Контроль страниц в таблице
        /// </summary>
        public PageManager Page { get; set; }
    }

    /// <summary>
    /// Индексированное представление для Таблицы 2
    /// </summary>
    public class ViewTable2Model<T>: IViewTableModel<T> where T:Table2
    {
        /// <summary>
        /// Таблица с данными
        /// </summary>
        public IEnumerable<T> Items { get; set; }

        /// <summary>
        /// Просто произвольное значение
        /// </summary>
        public int SomeValue { get; set; }

        /// <summary>
        /// Контроль страниц в таблице
        /// </summary>
        public PageManager Page { get; set; }
    }

    /// <summary>
    /// Индексированное представление для Таблицы 3
    /// </summary>
    public class ViewTable3Model<T> : IViewTableModel<T> where T:Table3
    {
        /// <summary>
        /// Таблица с данными
        /// </summary>
        public IEnumerable<T> Items { get; set; }

        /// <summary>
        /// Просто произвольное значение
        /// </summary>
        public int SomeValue { get; set; }

        /// <summary>
        /// Контроль страниц в таблице
        /// </summary>
        public PageManager Page { get; set; }
    }
}
