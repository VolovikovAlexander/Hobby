using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Rubezh.Core
{
    /// <summary>
    /// Класс реализация одного знания
    /// </summary>
    [Serializable]
    public class Building : Common, IBuilding
    {
        protected List<BuildingFloor> _floors = new List<BuildingFloor>();

        /// <summary>
        /// Этажи в строение
        /// </summary>
        [XmlArray("Floors")]
        [XmlArrayItem("Item")]
        public List<BuildingFloor> Floors { get => _floors; set => _floors = value; }
        public string Name { get; set; }
        public string State { get; set; }

        /// <summary>
        /// При изменении состава меняем устройства
        /// </summary>
        /// <returns></returns>
        public void CreateDevices()
        {
            _floors.ForEach(x =>
            {
                x.CreateDevices();
            });
        }
    }
}
