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

        /// <summary>
        /// Уникальный код хоста
        /// </summary>
        public int UniqueCode { get; set; }

        /// <summary>
        /// Хост
        /// </summary>
        public string Host { get => _host; set
            {
                _host = value;
                if (!string.IsNullOrEmpty(_host))
                {
                    var items = _host.Split('.').AsEnumerable();
                    // Сортируем в обратном порядке
                    Items = items
                            .Select((x, index) => new { host = x, position = index })
                            .OrderByDescending(x => x.position)
                            .Select(x => x.host)
                            .AsEnumerable();
                }
            }
        }

        /// <summary>
        /// Состав хоста
        /// </summary>
        public IEnumerable<string> Items { get; set; }

        /// <summary>
        /// Ссылка на предыдущий хост
        /// </summary>
        public HostItem Parent { get; set; }
    }
}
