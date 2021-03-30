using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqToDB;
using LinqToDB.Common;
using System.Threading;
using MaxMind.GeoIP2;

namespace Hybrid.Core
{
    /// <summary>
    /// Реализация основного класса для работы с IP адресами
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class HybridManager<T> : IHybridManager<T> where T: IIPAddressLocation
    {
        private static readonly Object _lockObject = new Object();


        #region "Конструкторы"
        public HybridManager()
        {
            // Сразу проверку запускаю
            Task.Run(() => Run());
        }

        #endregion

        #region "Работа с ошибками"

        protected string _errorText = "";
        public string ErrorText { get => _errorText; set { _errorText = value; } }

        public bool IsError => (string.IsNullOrEmpty(_errorText) ? false : true);

        #endregion

        #region "Поддержка интерфейса IHybridManager"

        /// <summary>
        /// Получить полный список IP адресов - только последнии
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetIPAddress()
        {
            _errorText = "";
            using (var dbContext = new DataDbConnection())
            {
                try
                {
                    // Получить общий список
                    var list = (from s in dbContext.IPAddresses
                                join p in dbContext.Locations on s.ID equals p.IpAddressID into ps
                                from p in ps.DefaultIfEmpty()
                                select new IPAddressLocation()
                                {
                                    IPAddress = s.IPAddress,
                                    CheckPeriod = s.Period,
                                    Location = p.Location ?? ""
                                }) ;
                    
                    // Выбрать из него только с максимальной датой
                    var tryItems = list.GroupBy(x => x.IPAddress)
                               .Select(x => new IPAddressLocation()
                               {
                                   IPAddress = x.Key,
                                   CheckPeriod = x.Max(y => y.CheckPeriod)
                               });

                    // Конвертируем и накладываем условия
                    IQueryable<T> items = (IQueryable<T>)list.Where(x => tryItems.Any(y => y.IPAddress == x.IPAddress && y.CheckPeriod == x.CheckPeriod));
                    return items.ToList().AsQueryable();
                }
                catch (Exception ex)
                {
                    _errorText = $"Ошибка при работе с базой данных! Системное описание {ex.Message}";
                }
            }

            return null;
        }

        /// <summary>
        /// Получить информацию по конкретному IP адресу (последнюю)
        /// </summary>
        /// <param name="IpInfo"></param>
        /// <returns></returns>
        public T GetIPInfo(T IpInfo)
        {
            _errorText = "";

            // Делаем проверку на локацию списка IP адресов
            Task.Run(() => Run());

            using (var dbContext = new DataDbConnection())
            {
                try
                {
                    if ( dbContext .IPAddresses.Any(x => x.IPAddress == IpInfo.IPAddress) )
                    {
                        var item = this.GetIPAddress().Where(x => x.IPAddress == IpInfo.IPAddress).FirstOrDefault();
                        return item;
                    }
                    else
                        // Ничего не найдено. Добавим в базу для уточнения сразу же
                        dbContext.Insert( new IPAddressesMap() { IPAddress = IpInfo.IPAddress, Period = DateTime.Now.AddHours(-5), DateAdd = DateTime.Now });
                }
                catch (Exception ex)
                {
                    _errorText = $"Ошибка при работе с базой данных! Системное описание {ex.Message}";
                }
            }

            return default(T);
        }

        /// <summary>
        /// Выполнить запрос на получение локаций по IP адресам
        /// </summary>
        public virtual void Run()
        {
            _errorText = "";

            var items = GetIPAddressForCheck();
            if (items == null) return;

            if(items.Count() > 0)
            {
                bool _lock = false;
                try
                {
                    Monitor.Enter(_lockObject, ref _lock);
                    List<Task> _tasks = new List<Task>();

                    foreach (var item in items)
                    {
                        var result = GetIPLocation(item.IPAddress);
                        _tasks.Add(result);
                    }

                    Task.WaitAll(_tasks.ToArray());
                }
                catch(Exception ex)
                {
                    _errorText = $"Ошибка при работе с потоками {ex.Message}";
                }
                finally
                {
                    if (_lock)
                    {
                        Monitor.Exit(_lockObject);
                    }
                }
            }
        }

        /// <summary>
        /// Получить список IP адресов для проверки локации
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetIPAddressForCheck()
        {
            _errorText = "";
            using (var dbContext = new DataDbConnection())
            {
                try
                {
                    var list = from s in dbContext.IPAddresses
                               join p in dbContext.Locations on s.ID equals p.IpAddressID into ps
                               from p in ps.DefaultIfEmpty()
                               where s.Period <= DateTime.Now.AddHours(-5)
                               select new IPAddressLocation()
                               {
                                   IPAddress = s.IPAddress,
                                   CheckPeriod = s.Period,
                                   Location = p.Location ?? ""
                               };

                    IQueryable<T> items = (IQueryable<T>)(list.ToList().AsQueryable());
                    return items;
                }
                catch (Exception ex)
                {
                    _errorText = $"Ошибка при работе с базой данных! Системное описание {ex.Message}";
                }
            }

            return null;
        }

        #endregion

        /// <summary>
        /// Получить локацию по IP адресу
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        private async Task GetIPLocation(string ipAddress)
        {
            // Account / User ID
            //160966
            //License key
            //WzuLLITL0SVkdqTZ

            _errorText = "";
                       
            if (!string .IsNullOrEmpty(ipAddress) && IPAddressLocation.CheckIp(ipAddress))
            {
                string locationInfo = "";
                var client = new WebServiceClient(160966, "WzuLLITL0SVkdqTZ");

                var cityResponce = client.City(ipAddress);
                locationInfo += $"City {cityResponce.City.Name}, Country {cityResponce.Country.Name}";

                using (var dbContext = new DataDbConnection())
                {
                    try
                    {
                        // Добавим запись о локации
                        var item = (from s in dbContext.IPAddresses where s.IPAddress == ipAddress orderby s.Period descending select s).FirstOrDefault();
                        if (item != null)
                            await dbContext.InsertAsync(new LocationsMap() { IpAddressID = item.ID, Location = locationInfo, DateAdd = DateTime.Now });
                    }
                    catch (Exception ex)
                    {
                        _errorText = $"Ошибка при выполнении операции с базой данных! {ex.Message}";
                    }
                }
            }
        }


        // the end
    }
}
