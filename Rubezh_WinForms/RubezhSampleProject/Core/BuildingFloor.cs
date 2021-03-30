using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Linq;

namespace Rubezh.Core
{
    /// <summary>
    /// Реализация описание одного эатажа одного здания
    /// </summary>
    [Serializable]
    public class BuildingFloor : Common, IBuildingFloor
    {
        protected List<Room> _rooms = new List<Room>();
        public int Number { get; set; }

        /// <summary>
        /// Список комнат
        /// </summary>
        [XmlArray("Rooms")]
        [XmlArrayItem("Item")]
        public List<Room> Rooms { get => _rooms; set => _rooms = value; }

        /// <summary>
        /// При изменении состава меняем устройства
        /// </summary>
        /// <returns></returns>
        public void CreateDevices()
        {
            Rooms.ForEach(x =>
            {
                x.CreateDevices();
            });
        }
    }
}
