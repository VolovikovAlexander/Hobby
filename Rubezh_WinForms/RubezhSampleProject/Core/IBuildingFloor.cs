using System;
using System.Collections.Generic;
using System.Text;

namespace Rubezh.Core
{
    /// <summary>
    /// Описание одного этажа одного строения
    /// </summary>
    public interface IBuildingFloor: ICommon
    {
        /// <summary>
        /// Номер этажа
        /// </summary>
        int Number { get; set; }

        /// <summary>
        /// Набор комнат
        /// </summary>
        List<Room> Rooms { get; set; }

    }
}
