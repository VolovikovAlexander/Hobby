using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace ControlPoint.Controllers
{
    using ControlPoint.Core;
    using ControlPoint.Models;


    /// <summary>
    /// Контрол для предоставления данных Таблица 2
    /// </summary>
    [ViewComponent(Name = "Table3Component")]
    public class Table3Component : ViewComponent
    {
        /// <summary>
        /// Вызвать инициализацию компонента асинхронно
        /// </summary>
        /// <returns></returns>
        public IViewComponentResult Invoke(int page = 1)
        {
            var manager = WindsorContainerHelper.GetService<IViewTableManager<Table3>>();
            var result = manager.Index(page);
            result.SomeValue = 3;
            return View(result);
        }
    }

}
