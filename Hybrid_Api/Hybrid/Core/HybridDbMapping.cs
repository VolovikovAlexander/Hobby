using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqToDB.Mapping;
using LinqToDB;
using LinqToDB.Common;
using LinqToDB.Data;

namespace Hybrid.Core
{
    /// <summary>
    /// Таблица для хранения истории запросов и кэширования
    /// </summary>
    [Table(Name = "IPAddresses")]
    public class IPAddressesMap
    {
        private DateTime _period = new DateTime(1900, 1, 1);

        [PrimaryKey, Identity]
        public long ID { get; set; }

        [Column(Name = "IPAddress"), NotNull]
        public string IPAddress { get; set; }


        /// <summary>
        /// Дата вставки записи
        /// </summary>
        [Column(Name = "DateAdd"), NotNull]
        public DateTime DateAdd { get; set; }

        /// <summary>
        /// Дата обновления записи
        /// </summary>
        [Column(Name = "Period"), NotNull]
        public DateTime Period { get => _period; set { _period = value; } }
    }

   
    /// <summary>
    /// Таблица для хранения истории локации по IP адресам
    /// </summary>
    [Table(Name = "Locations")]
    public class LocationsMap
    {
     
        [PrimaryKey, Identity]
        public long ID { get; set; }

        [Column(Name = "IpAddressID"), NotNull]
        public long IpAddressID { get; set; }

        [Column(Name = "Location"), NotNull]
        public string Location { get; set; }

        [Column(Name = "DateAdd"), NotNull]
        public DateTime DateAdd { get; set; }
    }

    /// <summary>
    /// Основной класс с описанием таблиц
    /// </summary>
    public class DataDbConnection : DataConnection
    {
        public DataDbConnection() { }

        /// <summary>
        /// Таблица с IP адресами
        /// </summary>
        public ITable<IPAddressesMap> IPAddresses => GetTable<IPAddressesMap>();

        /// <summary>
        /// Связанная таблица с информацией о локации по IP адресу
        /// </summary>
        public ITable<LocationsMap> Locations => GetTable<LocationsMap>();
    }
}
