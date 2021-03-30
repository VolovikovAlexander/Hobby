using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;

namespace API.Core
{
    /////////////////////////////////////////////////////////////////
    // Интерфейсы для работы с файлами и таблицей

    
    /// <summary>
    /// Общий интерфейс
    /// </summary>
    public interface ICommon
    {
        string ErrorText { get; set; }

    }


    /// <summary>
    /// Собственный класс аттрибут
    /// </summary>
    public class DelimiterAttribute : Attribute
    {
        public string Name { get; private set; }

        public DelimiterAttribute(string name)
        {
            Name = name.Trim();
        }
    }



    /// <summary>
    /// Типы разделителей
    /// </summary>
    public enum DelimetrType
    {
        /// <summary>
        /// ,
        /// </summary>
        [Delimiter(",")]
        Comma = 1,

        /// <summary>
        /// |
        /// </summary>
        [Delimiter("|")]
        Splash = 2,

        /// <summary>
        /// Табуляция
        /// </summary>
        [Delimiter("***")]
        Tab = 3
    }

    /// <summary>
    /// Данные по файлу
    /// </summary>
    public interface IApiFileInfo
    {
        /// <summary>
        /// Наименование файла
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Исходное значение файла
        /// </summary>
        string Body { get; set; }

        /// <summary>
        /// Тип разделителя колонок
        /// </summary>
        DelimetrType type { get; set; }
    }

    /// <summary>
    /// Информация об одной записи распакованной из файла
    /// </summary>
    public interface IApiTableRowInfo
    {
        List<object> values { get; set; }
    }


    /// <summary>
    /// Информация о полученной таблице
    /// </summary>
    public interface IApiTableInfo
    {
        ApiFileInfo file { get; set; }

        List<ApiTableRowInfo> rows { get; set; }

        List<string> columns { get; set; }
    }
}
