using System;
using System.Collections.Generic;
using System.Text;

namespace Rubezh.Core
{
    /// <summary>
    /// Интерфейс для описания одного устройства 
    /// </summary>
    public interface IDevice
    {
        /// <summary>
        /// Некий артукул устройства - уникальный
        /// </summary>
        string Article { get; set; }

        // Координаты устройства на потолку
        double X { get; set; }

        double Y { get; set; }

    }
}
