using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.AspNetCore.Mvc;
using LinqToDB.Configuration;
using System.Runtime.CompilerServices;
using LinqToDB.Data;
using System.Linq;
using Microsoft.AspNetCore.Cors;
using System.Security.Cryptography.X509Certificates;
using LinqToDB;

namespace RestApi
{
    /// <summary>
    /// Базовый контроллер для REST API
    /// </summary>
    [ApiController]
    [Route("api")]

    public class BaseController : ControllerBase
    {
        public BaseController()
        {
        }

        /// <summary>
        /// Получить список всех цветов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetColors")]
        [EnableCors()]
        public IEnumerable<dbRefColors> GetColors()
        {
            using (var context = new DbContext())
            {
                var result = context.Colors.ToList();
                return result;
            }
        }


        /// <summary>
        /// Получить список всех напитков
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetDrinks")]
        [EnableCors()]
        public IEnumerable<dbRefDrinks> GetDrinks()
        {
            using (var context = new DbContext())
            {
                var result = context.Drinks.ToList();
                return result;
            }
        }

        /// <summary>
        /// Получить данные для отображения конкретной анкеты
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetMainForm")]
        [EnableCors()]
        public IEnumerable<Dto.MainFormTablePreview> GetMainForm(int MainID)
        {
            var list = Dto.MainFormTablePreview.ToList();
            var result = Enumerable.Empty<Dto.MainFormTablePreview>();
            if (MainID <= 0)
                result = list.Select(x => new Dto.MainFormTablePreview()
                {
                    MainID = x.MainID,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Phone = x.Phone,
                    BirthdayString = x.BirthdayString
                }).Distinct().ToList();
            else
                result = list.Where(x => x.MainID == MainID).ToList();

            return result;
        }

        /// <summary>
        /// Удалить анкету
        /// </summary>
        /// <param name="MainID"></param>
        [HttpGet]
        [Route("DeleteMainForm")]
        [EnableCors()]
        public void DeleteMainForm(int MainID)
        {
            using (var context = new DbContext())
            {
                if (context.MainForm.Any(x => x.ID == MainID))
                {
                    using (var tran = context.BeginTransaction())
                    {
                        context.MainForm.Where(x => x.ID == MainID).Delete();
                        tran.Commit();
                    }
                }
            }
        }


        /// <summary>
        /// Добавить анкету
        /// </summary>
        /// <param name=""></param>
        [HttpPost]
        [Route("AddMainForm")]
        [EnableCors()]
        public RestApi.Dto.Result AddMainForm([FromForm] RestApi.Dto.MainForm mainForm)
        {
            if(mainForm != null)
            {
                using (var context = new DbContext())
                {
                    using (var tran = context.BeginTransaction())
                    {
                        // Добавим новую запись
                        var _mainForm = new dbTblMainForm(mainForm);
                        context.Insert (_mainForm);
                        tran.Commit();

                        // TODO: Тут тоже не верно. Луше переделать! 
                        var _id = context.MainForm.ToList().LastOrDefault().ID;
                        return new Dto.Result() { ID = _id };
                    }
                }
            }

            return new Dto.Result();
        }


        /// <summary>
        /// Добавить выбор пользователя
        /// </summary>
        /// <param name="selectForm"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddSelect")]
        [EnableCors()]
        public RestApi.Dto.Result AddSelect([FromForm] Dto.MainForm_Select selectForm)
        {
            if (selectForm != null)
            {
                using (var context = new DbContext())
                {
                    using (var tran = context.BeginTransaction())
                    {
                        var mainID = selectForm.MainID;

                        // Выбор цветов
                        selectForm.SelectColors.Where(x => x.Select == true && x.ColorID > 0)
                            .ToList()
                            .ForEach(x => 
                            {
                                var item = new dbTblMainFormSelectionResult()
                                { ColorID = x.ColorID, MainID = mainID };
                                context.Insert(item);
                            });

                        // Выбор напитков
                        selectForm.SelectDrinks.Where(x => x.Select == true && x.DrinkID > 0)
                            .ToList()
                            .ForEach(x =>
                            {
                                var item = new dbTblMainFormSelectionResult()
                                { DrinkID = x.DrinkID, MainID = mainID };
                                context.Insert(item);
                            });

                        tran.Commit();
                        // TODO: Тут нужен откат добавить!
                        return new Dto.Result() { ID = int.MaxValue };
                    }
                }
            }
            else return new Dto.Result();
        }

    }
}
