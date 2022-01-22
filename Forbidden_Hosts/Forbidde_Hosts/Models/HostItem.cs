using System.Collections.Generic;
using System.Linq;

namespace Forbidden_Hosts
{
    /// <summary>
    /// Описание хоста
    /// </summary>
    public class HostItem
    {
        private string _host = "";
        private IEnumerable<string> _findStataments = Enumerable.Empty<string>();

        /// <summary>
        /// Уникальный код хоста
        /// </summary>
        public int UniqueCode { get; set; }

        /// <summary>
        /// Хост
        /// </summary>
        public string Host { 
            get  => _host; 
            set
            {
                _host = value;
                _findStataments = value.BuildFindStataments();
            }
        }

      
        /// <summary>
        /// Получить список хостов для поиска данных.
        /// </summary>
        public IEnumerable<string> FindStataments { get => _findStataments; }
    }
}
