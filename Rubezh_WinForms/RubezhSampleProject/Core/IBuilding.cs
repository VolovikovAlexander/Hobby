using System;
using System.Collections.Generic;
using System.Text;

namespace Rubezh.Core
{
    /// <summary>
    /// Описание одного строения
    /// </summary>
    public interface IBuilding: ICommon
    {
        /// <summary>
        /// Список этажей
        /// </summary>
        List<BuildingFloor> Floors { get; set; }

        /// <summary>
        /// Натменование строения
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Состояние строения
        /// </summary>
        string State { get; set; }

    }
}
