using System;
using System.Collections.Generic;
using System.Text;

namespace Rubezh.Core
{
    /// <summary>
    /// Реализация одного устройства
    /// </summary>
    public class Device : Common, IDevice
    {
        private string _article = "";

        public Device()
        {
            var _rnd = new Random();
            _article = _rnd.Next(1000, 9999).ToString();
        }
        public string Article { get => _article; set => _article = value; }

        public double X { get; set; }

        public double Y { get; set; }

        public override string ToString()
        {
            return $"Устройство {Article} (X:{X},Y:{Y})";
        }
    }
}
