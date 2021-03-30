using System;
using System.Collections.Generic;
using System.Text;

namespace Rubezh.Core
{
    /// <summary>
    /// Основной интерфейс для наследования
    /// </summary>
    public interface ICommon
    {
        string ErrorText { get; set; }

        bool IsError { get; }
    }
}
