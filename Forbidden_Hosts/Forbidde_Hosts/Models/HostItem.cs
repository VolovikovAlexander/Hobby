namespace Forbidden_Hosts
{
    /// <summary>
    /// Описание хоста
    /// </summary>
    public class HostItem
    {
        /// <summary>
        /// Уникальный код хоста
        /// </summary>
        public int UniqueCode { get; set; }

        /// <summary>
        /// Хост
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Ссылка на предыдущий хост
        /// </summary>
        public HostItem Parent { get; set; }
    }
}
