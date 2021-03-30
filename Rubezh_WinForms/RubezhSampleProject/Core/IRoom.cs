using System;
using System.Collections.Generic;
using System.Text;


namespace Rubezh.Core
{
    /// <summary>
    /// Интерфейс для работы с одной комнатой
    /// </summary>
    public interface IRoom: ICommon
    {
        /// <summary>
        /// Ширина комнаты
        /// </summary>
        double Height { get; set; }

        /// <summary>
        /// Высота комнаты
        /// </summary>
        double Weight { get; set; }

        /// <summary>
        /// Длина комнаты
        /// </summary>
        double Lenght { get; set; }

        /// <summary>
        /// Список устройств в комнате
        /// </summary>
        List <IDevice> Devices { get; }

        /// <summary>
        /// Расположить устройства согласно параметрам
        /// </summary>
        /// <returns></returns>
        void CreateDevices();
    }
}
