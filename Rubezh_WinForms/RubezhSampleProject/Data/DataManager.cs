using System;
using System.Collections.Generic;
using System.Text;
using Rubezh.Core;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Linq;
using System.Data;


namespace Rubezh.Data
{
    /// <summary>
    /// Основной класс для работы с данными = XML
    /// </summary>
    public class DataManager: Common
    {
        private string _sourceFile = "";

        // Список строений
        private List<Building> _items = new List<Building>();


        #region "Конструкторы"
        public DataManager()
        { }

        /// <summary>
        /// Констркутор с автоматической загрузкой данных
        /// </summary>
        /// <param name="sourceFile"></param>
        public DataManager(string sourceFile)
        {
            _sourceFile = sourceFile;
            LoadData(_sourceFile);
        }

        #endregion

        #region "Формирование отборов"

        /// <summary>
        /// Получить ссылку на комнату по уникальному коду
        /// </summary>
        /// <param name="sourceCode"></param>
        /// <returns></returns>
        public Room GetRoomRef(int sourceCode)
        {
            var items = _items.SelectMany(x => x.Floors.Select(y => y.Rooms.Where(p => p.Code == sourceCode).FirstOrDefault()));
            return items.Where(x => x != null).FirstOrDefault();
        }

        /// <summary>
        /// Получить список ошибочных комнат
        /// </summary>
        /// <returns></returns>
        public List<int> GetRoomWithErrors()
        {
            if (_items.Any())
            {
                var errorsList = new List<int>();
                _items
                    .ForEach(x =>
                    {
                        x.Floors.ForEach(y =>
                        {
                            errorsList.AddRange(y.Rooms.Where(z => z.IsError).Select(z => z.Code).ToList());
                        });
                    });

                return errorsList;
            }

            return new List<int>(); 
        }

        #endregion


        /// <summary>
        /// Список всех строений
        /// </summary>
        public List<Building> Building { get => _items; }

        #region "Работа с данными"

        public DataTable GetTable()
        {
            var _table = new DataTable("Result");
            var columns = (new List<string>() { "BuildingName", "FloorNumber", "RoomNumber", "Height", "Weight",
                "Lenght", "DeviceCount" ,"IsError", "Code"}).Select(x => new DataColumn()
                {
                    ColumnName = x,
                    DataType = typeof(string)
                })
                .ToArray();
            _table.Columns.AddRange(columns);

            _items.ForEach(x =>
            {
                x.Floors.ForEach(y =>
                {
                    y.Rooms.ForEach(p =>
                    {
                        var roomNumber = 1;

                        var row = _table.NewRow();
                        row["BuildingName"] = x.Name;
                        row["FloorNumber"] = y.Number;
                        row["RoomNumber"] = roomNumber;
                        row["Height"] = p.Height;
                        row["Weight"] = p.Weight;
                        row["Lenght"] = p.Lenght;
                        row["DeviceCount"] = p.Devices.Count();
                        row["IsError"] = p.IsError;
                        row["Code"] = p.Code;

                        _table.Rows.Add(row);
                    });
                });
            });


            return _table;
        }

        /// <summary>
        /// Сформировать по всей цепочки устройства
        /// </summary>
        private void CreateDevices()
        {
            if(_items.Any())
            {
                _items.ForEach(x =>
                {
                    x.CreateDevices();
                });
            }
        }

        /// <summary>
        /// Сформировать тестовые данные
        /// </summary>
        public void GenerateSampleData()
        {
            var _random = new Random();

            // Формируем до 10 зданий
            var items = Enumerable.Range(1, _random.Next(10))
                .ToList()
                .Select(x => new Building()
                { 
                    Name = $"Строение - {x}",
                    State = "Ready"
                })
                .ToList();

            items
                .ForEach(x =>
                {
                    // Формируем до 5 этажей
                    var floors = Enumerable.Range(1, _random.Next(5))
                    .ToList()
                    .Select(p => new BuildingFloor()
                    {
                        Number = p
                    })
                    .ToList();

                    floors
                        .ForEach(p =>
                    {
                        var _rooms = Enumerable.Range(0, _random.Next(10))
                            .ToList()
                            .Select(y => new Room()
                            {
                                Height = _random.Next(50),
                                Weight = _random.Next(50),
                                Lenght = _random.Next(50),
                                Code = _random.Next(1000, 999999)
                            }).ToList();


                        // Формируем до 10 комнат на кажом этаже
                        p.Rooms = _rooms;
                    });

                    x.Floors = floors;

                    // Добавим в список
                    _items.Add(x);
                });

            CreateDevices();
        }

        #endregion

        /// <summary>
        /// Загрузить тестовые данные
        /// </summary>
        public void LoadData(string sourceFile)
        {
            _errorText = "";
            _sourceFile = sourceFile;

            if (!File.Exists(_sourceFile))
                _errorText = $"Не найден файл {_sourceFile}";
            else
            {
                try
                {
                    var serializer = new XmlSerializer(typeof(List<Building>));
                    _items.Clear();

                    using (var stream = File.OpenRead(_sourceFile))
                    {
                        _items = (List<Building>)serializer.Deserialize(stream);
                        CreateDevices();
                    }
                }
                catch (Exception ex)
                {
                    _errorText = $"Ошибка при загрузке файла {_sourceFile}. Описание {ex.Message}";
                }
            }
        }

        /// <summary>
        /// Сохранить данные в файл
        /// </summary>
        /// <param name="sourceFile"></param>
        public void SaveData(string sourceFile)
        {
            _sourceFile = sourceFile;
            if (!_items.Any())
                GenerateSampleData();

            try
            {
                var serializer = new XmlSerializer(typeof(List<Building>));
                using (var stream = File.OpenWrite(sourceFile))
                {
                    serializer.Serialize(stream, _items);
                }
            }
            catch (Exception ex)
            {
                _errorText = $"Ошибка при загрузке файла {_sourceFile}. Описание {ex.Message}";
            }
            
        }

        /// <summary>
        /// Название файла для загрузки данных
        /// </summary>
        public string FileName
        {
            get => _sourceFile; 
            set
            {
                _sourceFile = value.Trim();
                LoadData(_sourceFile);
            }
        }
    }
}
