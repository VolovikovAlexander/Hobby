using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Xml.Serialization;
using System.Linq;

namespace Rubezh.Core
{
    /// <summary>
    /// Реализация одной комнаты
    /// </summary>
    [Serializable]
    public class Room : Common, IRoom
    {
        protected List<IDevice> _devices = new List<IDevice>();
        private int _uniqueNumber = 0;

        public Room()
        {
        }

        /// <summary>
        /// Автоматический уникальный код
        /// </summary>
        public int Code { get => _uniqueNumber; set => _uniqueNumber = value; }
        public double Height { get; set; }
        public double Weight { get; set; }

        public double Lenght { get; set; }

        /// <summary>
        /// Список устройств - вычисляется
        /// </summary>
        [XmlIgnore]
        public List<IDevice> Devices => _devices;

        /// <summary>
        /// Сформировать список устройств
        /// </summary>
        /// <returns></returns>
        public void CreateDevices()
        {
            double _stepDevice = 0.0;
            double _stepBox = 0.0;

            if(Weight <= 3.5)
            {
                _stepDevice = 5.0;
                _stepBox = 2.5;
            }
            else
            {
                _stepDevice = 4.5;
                _stepBox = 2.0;
            }

            if(Height == 0 || Weight == 0 || Lenght == 0)
            {
                _errorText = "Не верные размеры комнаты!";
                return;
            }


            if( (Weight / _stepBox) < 0 )
            {
                _errorText = "Ширина комнаты не соответствует стандарту. Не возможно установить устройство!";
                return;
            }

            var countDevices = Convert.ToInt32(( (Lenght - (2 * _stepBox)) / _stepDevice) + 1);
            var _startPosition = _stepBox;

            // Формируем устройства
            Enumerable.Range(1, countDevices)
                .ToList()
                .ForEach(x =>
                {
                    var _device = new Device();
                    // В центр
                    _device.X = Weight / 2;
                    _device.Y = _startPosition;

                    _startPosition += _stepDevice;
                    _devices.Add(_device);
                });

            if (!_devices.Any())
                _errorText = "Не возможно расположить устройство по указанным критериям!";
        }


        public override string ToString()
        {
            var result = $"Комната {_uniqueNumber}" + Environment.NewLine + $"{(IsError ? _errorText : "")}";
            result += "Список устройств: " + Environment.NewLine +
                string.Join(Environment.NewLine, _devices.Select(x => x.ToString()).ToArray());

            return result;
        }
    }
}
