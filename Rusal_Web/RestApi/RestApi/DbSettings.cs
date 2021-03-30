using System;
using System.Collections.Generic;
using System.Linq;
using LinqToDB.Configuration;

namespace RestApi
{
    /// <summary>
    /// Класс описание настройки подключения
    /// </summary>
    public class ConnectionStringSettings : IConnectionStringSettings
    {
        public string ConnectionString { get; set; }
        public string Name { get; set; }
        public string ProviderName { get; set; }
        public bool IsGlobal { get; set; }
    }

    /// <summary>
    /// Класс реализация вариантов настроек подключения
    /// </summary>
    public class ApiSettings : ILinqToDBSettings
    {
        public IEnumerable<IDataProviderSettings> DataProviders => Enumerable.Empty<IDataProviderSettings>();

        public string DefaultConfiguration => "SqlServer";
        public string DefaultDataProvider => "SqlServer";

        /// <summary>
        /// Список всег соединений
        /// </summary>
        public IEnumerable<IConnectionStringSettings> ConnectionStrings
        {
            get
            {
                yield return
                    new ConnectionStringSettings
                    {
                        Name = "RusalWebSystem",
                        ProviderName = "SqlServer",
                        ConnectionString = @"Server=localhost\sqlexpress;Database=RusalWebSystem;Trusted_Connection=True;Enlist=False;",
                        IsGlobal = true
                    };
            }
        }

        /// <summary>
        /// По умолчанию соединение
        /// </summary>
        public IConnectionStringSettings Default => ConnectionStrings.Where(x => x.IsGlobal == true).FirstOrDefault();
    }
}
