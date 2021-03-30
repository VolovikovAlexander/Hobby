using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqToDB.Mapping;
using LinqToDB;
using LinqToDB.Common;
using LinqToDB.Data;

namespace ZipInfo.Core
{
    /// <summary>
    /// Таблица для хранения истории запросов и кэширования
    /// </summary>
    [Table(Name = "ZipInfo")]
    public class ZipInfoMap
    {
        [PrimaryKey]
        public long ZipCode { get; set; }

        [Column(Name = "CityName"), NotNull]
        public string CityName { get; set; }

        [Column(Name = "TimeZone"), NotNull]
        public string TimeZone { get; set; }

        /// <summary>
        /// Дата вставки записи
        /// </summary>
        [Column(Name = "DateAdd"), NotNull]
        public DateTime DateAdd { get; set; }
    }

    /// <summary>
    /// Типы API через которые мы будем делать запросы
    /// </summary>
    public class HostApiType
    {
        public enum TypeApi
        {
            /// <summary>
            /// Работа с погодой по ZIP коду
            /// </summary>
            WeatherMap = 1,

            /// <summary>
            /// Работа с временной зоной 
            /// </summary>
            Google = 2
        };

        public string Host { get; set;}

        public TypeApi Api { get; set; }
    }

    /// <summary>
    /// Таблица для отслеживания состояния запросов к API
    /// </summary>
    [Table(Name = "ZipInfoApi")]
    public class ZipInfoMapApi
    {
        private string _errorText = "";

        [PrimaryKey, Identity]
        public long ID { get; set; }

        [Column(Name = "HostType"), NotNull]
        public int HostType { get; set; }

        [Column(Name = "Status"), NotNull]
        public int Status { get; set; }

        [Column(Name = "DateAdd"), NotNull]
        public DateTime DateAdd { get; set; }

        [Column(Name = "ErrorText")]
        public string ErrorText { get => _errorText; set { _errorText = value.Trim(); } }
    }

    /// <summary>
    /// Основной класс с описанием таблиц
    /// </summary>
    public class localZipConnection : DataConnection
    {
        public localZipConnection() { }
        public ITable<ZipInfoMapApi> ZipInfoApi => GetTable<ZipInfoMapApi>();
        public ITable<ZipInfoMap> ZipInfo => GetTable<ZipInfoMap>();
    }
}
