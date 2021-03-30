using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using LinqToDB;
using Newtonsoft.Json;


namespace ZipInfo.Core
{
    #region "Набор классов для приема структуры openweathermap - http://json2csharp.com/"
    public class Coord
    {
        public double lon { get; set; }
        public double lat { get; set; }

        public override string ToString()
        {
            return $"Мы находимся на следующих координатах {lon} X {lat}.";
        }
    }

    public class Weather
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }

        // Для отображения текущей погоды
        public override string ToString()
        {
            return $"Хъюстон! Докладываю. Сегодня {DateTime.Now.ToString("d")} - {main}, {description}";
        }
    }

    public class Main
    {
        public double temp { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
        public double temp_min { get; set; }
        public double temp_max { get; set; }
    }

    public class Wind
    {
        public double speed { get; set; }
        public int deg { get; set; }
        public double gust { get; set; }
    }

    public class Clouds
    {
        public int all { get; set; }
    }

    public class Sys
    {
        public int type { get; set; }
        public int id { get; set; }
        public double message { get; set; }
        public string country { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
    }

    public class openweathermapObject
    {
        public Coord coord { get; set; }
        public List<Weather> weather { get; set; }
        public string @base { get; set; }
        public Main main { get; set; }
        public int visibility { get; set; }
        public Wind wind { get; set; }
        public Clouds clouds { get; set; }
        public int dt { get; set; }
        public Sys sys { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public int cod { get; set; }

        /// <summary>
        /// Сообщение об ошибке, если не верный код передали в службу
        /// </summary>
        public string message { get; set; }
    }


    public class timezonedbObject
    {
        public string status { get; set; }
        public string message { get; set; }
        public string countryCode { get; set; }
        public string countryName { get; set; }
        public string zoneName { get; set; }
        public string abbreviation { get; set; }
        public int gmtOffset { get; set; }
        public string dst { get; set; }
        public int zoneStart { get; set; }
        public int zoneEnd { get; set; }
        public string nextAbbreviation { get; set; }
        public int timestamp { get; set; }
        public string formatted { get; set; }
    }

    #endregion

    /// <summary>
    /// Класс - описание для передачи данных на клиента
    /// </summary>
    public class ZipInfo
    {
        public string Weather { get; set; }

        public string TimeZone { get; set; }

        public string CityName { get; set; }

        public string Coordinate { get; set; }
    }

    /// <summary>
    /// Класс для работы со сторонними API
    /// </summary>
    public class ZipInfoManager
    {
        // Список API через которы мы работаем
        protected List<HostApiType> infoApis = new List<HostApiType>()
        {
            new HostApiType(){ Host = "http://api.timezonedb.com/v2.1/get-time-zone?key=8LJSVLVRANPF&format=json&by=position&lat={0}&lng={1}", Api= HostApiType.TypeApi.Google},
            // zip=94040,us
            new HostApiType(){ Host = "https://api.openweathermap.org/data/2.5/weather?zip={0},us&APPID=55c31bd52e0c787cf5c6bfb22f990eb6", Api = HostApiType.TypeApi.WeatherMap}
        };

        /// <summary>
        /// Типы статусов обмена информацией с хостами
        /// </summary>
        private enum HostStatus
        {
            None = 0,
            Error = 1,
            Success = 2
        }

        // Zip код
        private int _zipCode = 0;
        // Информация о погоде
        private Weather _weather;
        // Координаты населенного пункта
        private Coord _coord;
        

        // Полная информация
        private ZipInfo _zipInfo = new ZipInfo();

        // Сообщение об ошибке
        private string _errorText = "";

        protected HostApiType.TypeApi _currentType;
        protected string _currentHost;

        #region "Конструкторы"
        public ZipInfoManager()
        { }
      
        public ZipInfoManager(int zipCode)
        {
            _currentType = HostApiType.TypeApi.WeatherMap;
            _currentHost = infoApis.Where(x => x.Api == _currentType).Select(x => x.Host).FirstOrDefault();
            _zipCode = zipCode;
        }

        #endregion

        #region "Информация об ошибке"
        public bool IsError => (string.IsNullOrEmpty(_errorText) ? false : true);

        public string ErrorText => _errorText.Trim();

        #endregion

        #region "Выполнение запросов от API"

        /// <summary>
        /// Выполнить запрос к чужому API и получить данные
        /// </summary>
        public async Task Run()
        {
            await GetWheater();

            if (IsError)
            {
                 _zipInfo.CityName = $"Ошибка при выполнении запроса к хосту {_errorText}";
                  return;
            }

            // Проверить наличие временной зоны
            using (var localDb = new localZipConnection())
            {
                var result = (from s in localDb.ZipInfo where s.ZipCode == _zipCode select s).FirstOrDefault();
                if (result?.TimeZone != null)
                {
                    var timeZone = result.TimeZone;
                    _zipInfo.TimeZone = timeZone;
                    return;
                }
                else
                    await GetTimeZone();
            }
        }

        /// <summary>
        /// Получить данны о погоде
        /// </summary>
        private async Task GetWheater()
        {
            _errorText = "";

            if(_zipCode <= 0)
            {
                _errorText = "Не корректно передан ZIP код"!;
                return;
            }

            _currentType = HostApiType.TypeApi.WeatherMap;
            using (var localDb = new localZipConnection())
            {
                // 1. Определяем текущий статус запроса о погоде
                var currentStatus = (from s in localDb.ZipInfoApi where s.HostType == (int)_currentType orderby s.DateAdd descending select s.Status).FirstOrDefault();
                if(currentStatus != (int)HostStatus.Error)
                {
                    // 2. Делаем запрос на получение погоды по Zip коду
                    var url = infoApis.Where(x => x.Api == _currentType).Select(x => x.Host).FirstOrDefault();
                    url = string.Format(url, _zipCode);

                    try
                    {
                        HttpClient client = new HttpClient();
                        var response = await client.GetAsync(url);
                        string responseBody = await response.Content.ReadAsStringAsync();

                        // 3. Десериализуем 
                        var result = JsonConvert.DeserializeObject<openweathermapObject>(responseBody);

                        if (result.cod != 404)
                        {
                            // Получаем погоду
                            _weather = result.weather?.FirstOrDefault();
                            _coord = result.coord;

                            _zipInfo.Weather = _weather.ToString();
                            _zipInfo.CityName = result.name;
                            _zipInfo.Coordinate = result.coord.ToString();

                             // Записываем результат - ок
                             localDb.Insert(new ZipInfoMapApi() { HostType = (int)_currentType, Status = (int)HostStatus.Success, DateAdd = DateTime.Now });

                             // Добавим информацию о том, что уже по этому коду запрашивали
                             if (!localDb.ZipInfo.Any(x => x.ZipCode == _zipCode))
                                 localDb.Insert(new ZipInfoMap() { ZipCode = _zipCode, CityName = result.name, DateAdd = DateTime.Now });
                        }
                        else
                        {
                            _errorText = $"Ошибочный ответ. Почтовый индекс {_zipCode}. Сообщение от хоста {result.message}";
                             localDb.Insert(new ZipInfoMapApi() { HostType = (int)_currentType, Status = (int)HostStatus.Error, DateAdd = DateTime.Now, ErrorText = result.message });
                        }
                    }
                    catch (Exception ex)
                    {
                        // В историю добавим стутус - Ошибка
                        localDb.Insert(new ZipInfoMapApi() { HostType = (int)_currentType, Status = (int)HostStatus.Error, DateAdd = DateTime.Now, ErrorText = ex.Message });

                        _errorText = $"Ошибка при обращении к API. Системное описание {ex.Message}";
                         return;
                    }
                }
                else
                {
                     // Добавим новую запись для новой попытки
                     localDb.Insert(new ZipInfoMapApi() { HostType = (int)_currentType, Status = (int)HostStatus.None , DateAdd = DateTime.Now});

                     _errorText = "Не возможно произвести запрос на получение погоды. Сервис занят.";
                     return;
                }
            }
        }

        /// <summary>
        /// Получить данные о перенной зоне
        /// </summary>
        private async Task GetTimeZone()
        {
            _errorText = "";

            if (_zipCode <= 0)
            {
                _errorText = "Не корректно передан ZIP код"!;
                return;
            }

            _currentType = HostApiType.TypeApi.Google;
             using (var localDb = new localZipConnection())
             {
                // 1. Определяем текущий статус запроса о погоде
                var currentStatus = (from s in localDb.ZipInfoApi where s.HostType == (int)_currentType orderby s.DateAdd descending select s.Status).FirstOrDefault();
                if (currentStatus != (int)HostStatus.Error && _coord != null)
                {
                    // 2. Делаем запрос на временной зоны
                    var url = infoApis.Where(x => x.Api == _currentType).Select(x => x.Host).FirstOrDefault();
                    url = string.Format(url, _coord.lat, _coord.lon);

                    try
                    {
                        HttpClient client = new HttpClient();
                        var response = await client.GetAsync(url);
                        string responseBody = await response.Content.ReadAsStringAsync();

                        // 3. Десериализуем 
                        var result = JsonConvert.DeserializeObject<timezonedbObject>(responseBody);

                        if (result.status.ToUpper() == "OK")
                        {
                            var timeZone = result.gmtOffset / 3600;
                            _zipInfo.TimeZone = timeZone.ToString();
                            _zipInfo.Coordinate = _coord.ToString();

                            // Добавим информацию о том, что уже по этому коду запрашивали
                            if (localDb.ZipInfo.Any(x => x.ZipCode == _zipCode))
                                    localDb.ZipInfo.Update(x => x.ZipCode == _zipCode, x => new ZipInfoMap { TimeZone = timeZone.ToString() });
                         }
                    }
                    catch (Exception ex)
                    {
                        // В историю добавим стутус - Ошибка
                        localDb.Insert(new ZipInfoMapApi() { HostType = (int)_currentType, Status = (int)HostStatus.Error, DateAdd = DateTime.Now, ErrorText = ex.Message });

                        _errorText = $"Ошибка при обращении к API. Системное описание {ex.Message}";
                        return;
                    }
                }
                else
                {
                      // Добавим новую запись для новой попытки
                      localDb.Insert(new ZipInfoMapApi() { HostType = (int)_currentType, Status = (int)HostStatus.None, DateAdd = DateTime.Now });

                      _errorText = "Не возможно произвести запрос на получение погоды. Сервис занят.";
                      return;
                }
             }
        }

        #endregion

        #region "Свойства"
        /// <summary>
        /// Получить/ задать ZIP код
        /// </summary>
        public int ZipCode { get => _zipCode; set { if (value > 0) _zipCode = value; } }

        /// <summary>
        /// Информация для передачи на клиента
        /// </summary>
        public ZipInfo Info { get => _zipInfo; }

        #endregion
    }
}
