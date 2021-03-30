using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Castle.Windsor;

namespace ControlPoint.Controllers
{
    using ControlPoint.Models;
    using ControlPoint.Core;
    using Microsoft.EntityFrameworkCore;

    public class HomeController : Controller
    {
        // Подключаем контейнер
        private static readonly WindsorContainer _container = new WindsorContainer();
        public HomeController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        #region "Работа с первой таблицой"

        /// <summary>
        /// Получить данные по таблице 1 из компонента
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpPost]
        public IViewTableModel<Table1> GetTable1(int page = 1)
        {
            var manager = WindsorContainerHelper.GetService<IViewTableManager<Table1>>();
            if (manager != null)
            {
                var result = manager.Index(page);
                return result;
            }
            else
                return null;
        }

        /// <summary>
        /// Сформировать данные для таблицы 1 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IViewTableModel<Table1> BuildTable1()
        {
            // Получить сервис для генерации данных
            var process = WindsorContainerHelper.GetService<IBuildProcess<Table1>>();
            process.Execute(100);

            // Получить менеджер и данные
            var manager = WindsorContainerHelper.GetService<IViewTableManager<Table1>>();
            if (manager != null)
            {
                var result = manager.Index(1);
                return result;
            }
            else
                return null;
        }

        #endregion


        #region "Работа со второй таблицей"

        /// <summary>
        /// Получить данные по таблице 2 из компонента
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpPost]
        public IViewTableModel<Table2> GetTable2(int page = 1)
        {
            var manager = WindsorContainerHelper.GetService<IViewTableManager<Table2>>();
            if (manager != null)
            {
                var result = manager.Index(page);
                return result;
            }
            else
                return null;
        }

        /// <summary>
        /// Сформировать данные для таблицы 2 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IViewTableModel<Table2> BuildTable2()
        {
            // Получить сервис для генерации данных
            var process = WindsorContainerHelper.GetService<IBuildProcess<Table2>>();
            process.Execute(100);

            // Получить менеджер и данные
            var manager = WindsorContainerHelper.GetService<IViewTableManager<Table2>>();
            if (manager != null)
            {
                var result = manager.Index(1);
                return result;
            }
            else
                return null;
        }


        #endregion

        #region "Работа с третьей таблицей"

        /// <summary>
        /// Получить данные по таблице 3 из компонента
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpPost]
        public IViewTableModel<Table3> GetTable3(int page = 1)
        {
            var manager = WindsorContainerHelper.GetService<IViewTableManager<Table3>>();
            if (manager != null)
            {
                var result = manager.Index(page);
                return result;
            }
            else
                return null;
        }

        /// <summary>
        /// Сформировать данные для таблицы 3
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IViewTableModel<Table3> BuildTable3()
        {
            // Получить сервис для генерации данных
            var process = WindsorContainerHelper.GetService<IBuildProcess<Table3>>();
            process.Execute(100);

            // Получить менеджер и данные
            var manager = WindsorContainerHelper.GetService<IViewTableManager<Table3>>();
            if (manager != null)
            {
                var result = manager.Index(1);
                return result;
            }
            else
                return null;
        }


        #endregion
    }
}
