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
    /// Контрол для предоставления данных Таблица 1 
    /// </summary>
    [ViewComponent(Name = "Table1Component")]
    public class Table1Component: ViewComponent
    {
        /// <summary>
        /// Вызвать инициализацию компонента асинхронно
        /// </summary>
        /// <returns></returns>
        public IViewComponentResult Invoke(int page = 1)
        {
            var manager = WindsorContainerHelper.GetService<IViewTableManager<Table1>>();
            var result = manager.Index(page);
            result.SomeValue = 1;
            return View(result);
        }
    }
}
