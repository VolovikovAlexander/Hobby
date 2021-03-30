using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MaxMind.GeoIP2;

namespace Hybrid.Core
{
    /// <summary>
    /// Интерфейс для приема/ передачи данных
    /// </summary>
    public interface IIPAddressLocation
    {
        string IPAddress { get; set; }

        string Location { get; set; }

        DateTime CheckPeriod { get; set; }
    }

    /// <summary>
    /// Структура для возврата на клиент
    /// </summary>
    public class IPAddressLocation: IIPAddressLocation
    {
        protected string _ipAddress = "";
        protected DateTime _checkPeriod = new DateTime(1, 1, 1);

        public string IPAddress { get => _ipAddress; set { if (CheckIp(value)) _ipAddress = value; } }

        public string Location { get; set; }

        public DateTime CheckPeriod { get => _checkPeriod; set { _checkPeriod = value; } }

        /// <summary>
        /// Проверить корректность IPадреса
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public static bool CheckIp(string address)
        {
            var nums = address.Split('.');
            int useless;
            return nums.Length == 4 && nums.All(n => int.TryParse(n, out useless)) &&
               nums.Select(int.Parse).All(n => n < 256);
        }
    }

    /// <summary>
    /// Интерфейс для основного класса
    /// </summary>
    public interface IHybridManager<T> where T: IIPAddressLocation
    {
        /// <summary>
        /// Получить список IP адесов для обновления
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetIPAddressForCheck();

        /// <summary>
        /// Получить полный список IP адресов
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetIPAddress();


        /// <summary>
        /// Получить информацию по IP адресу
        /// </summary>
        /// <param name="IPAddress"></param>
        /// <returns></returns>
        T GetIPInfo(T IpInfo);

        /// <summary>
        /// Запустить процедуру поиска локации по IP адресам
        /// </summary>
        void Run();

        /// <summary>
        /// Описание ошибки
        /// </summary>
        string ErrorText { get; set; }

        /// <summary>
        /// Флаг - есть ошибка
        /// </summary>
        bool IsError { get; }
    }
}
