using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Rubezh.Core
{
    /// <summary>
    /// Реализация базового класса
    /// </summary>
    public class Common : ICommon
    {
        protected string _errorText = "";

        [XmlIgnore]
        public string ErrorText { get => _errorText; set => _errorText = value; }

        [XmlIgnore]
        public bool IsError => !string.IsNullOrEmpty(_errorText);
    }
}
