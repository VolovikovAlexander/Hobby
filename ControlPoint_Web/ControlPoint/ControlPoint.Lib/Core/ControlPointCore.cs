using Castle.Components.DictionaryAdapter;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using KeyAttribute = Castle.Components.DictionaryAdapter.KeyAttribute;
using System.ComponentModel.DataAnnotations.Schema;
using ControlPoint.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace ControlPoint.Core
{
    #region "Общие сущности и реализация"

    /// <summary>
    /// Общий интерфейс для всех сущностей
    /// </summary>
    public interface ICommon
    {
        /// <summary>
        /// Описание ошибки внутреннее 
        /// </summary>
        
        public string ErrorText { get; set; }

        /// <summary>
        /// Флаг. Есть ошибка
        /// </summary>
        public bool IsError { get; }

        /// <summary>
        /// Уникальный ключ для сущности
        /// </summary>
        public Int64 ID { get; set; }
    }

    /// <summary>
    /// Реализация общего интерфейса
    /// </summary>
    public class Common : ICommon
    {
        protected string _errorText = "";
        protected Int64 _Id = 0;

        [NotMapped]
        public string ErrorText { get => _errorText.Trim(); set => _errorText = value.Trim(); }

        [NotMapped]
        public bool IsError { get => !string.IsNullOrEmpty(_errorText); }

        [Key("ID")]
        public Int64 ID { get => _Id; set => _Id = value; }
    }

    /// <summary>
    /// Интерфейс к таблицам с данными
    /// </summary>
    public interface ITableData
    {
        /// <summary>
        /// Данные записи
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// Дата вставки записи
        /// </summary>
        public DateTime DateAdd { get; set; }
    }

    /// <summary>
    /// Общая сущность для описания таблиц с данными
    /// </summary>
    public class TableData : Common, ITableData
    {
        protected DateTime _dateAdd = DateTime.Now;
        public string Data { get; set; }
        public DateTime DateAdd { get => _dateAdd; set => _dateAdd = value; }
    }

    #endregion

    #region "Общие классы для поддержки сервисов и Windsor контейнера"

    /// <summary>
    /// Интерфейс к передаче табличных данных
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IViewTableManager<T> where T:ITableData
    {
        /// <summary>
        /// Получить данные по указанной странице
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public IViewTableModel<T> Index(int page = 1);
    }

    /// <summary>
    /// Интерфейс к основному процесс сервису
    /// </summary>
    public interface IBuildProcess<T> where T: ITableData
    {
        /// <summary>
        /// Сформировать записи синхронно
        /// </summary>
        /// <param name="soureCountRows"></param>
        public void Execute(int soureCountRows = 1000);
        
        /// <summary>
        /// Сформировать записи асинхронно
        /// </summary>
        /// <param name="soureCountRows"></param>
        /// <returns></returns>
        public Task<int> ExecuteAsync(int soureCountRows = 1000);

    }

    /// <summary>
    /// Абстрактный класс. Все кто от него наследуется будет
    /// автоматически добавлен к контейнер
    /// </summary>
    public abstract class WindsorAbstract { }

    
    /// <summary>
    /// Аттрибут, который указывает, что класс будет общим сервисом
    /// </summary>
    public class ServiceAttribute : Attribute { }

    #endregion

    #region "Классы для контейнера Windsor"

    /// <summary>
    /// Класс реализация процесс сервиса.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Service]
    public class BuildProcess<T>: WindsorAbstract, IBuildProcess<T> where T: ITableData, new()
    {
        /// <summary>
        /// Сформировать определенное количество записей в сущности T
        /// Синхронно
        /// </summary>
        /// <param name="soureCountRows"></param>
        public void Execute(int soureCountRows = 100)
        {
            if (soureCountRows <= 0)
                throw new Exception("Не коррекно переданы параметры в метод Execute!");

            // 1. Сформируем список
            var rnd = new Random();
            var items = Enumerable.Range(0, soureCountRows)
                .Select(x => new T()
                {
                    Data = "T_New_" + rnd.Next(99999).ToString(),
                    DateAdd = DateTime.Now
                }).ToList();

            // 2. Добавим записи
            using (var context = new ControlPointDbContext())
            {
                items.ForEach(x =>
                {
                    context.Add(x);
                });

                context.SaveChanges();
            }
        }

        /// <summary>
        /// Сформировать определенное количество записей в сущности T
        /// асинхронно
        /// </summary>
        /// <param name="soureCountRows"></param>
        /// <returns></returns>
        public async Task<int> ExecuteAsync(int soureCountRows = 100)
        {
           if(soureCountRows <= 0)
                throw new Exception("Не коррекно переданы параметры в метод Execute!");

            // 1. Сформируем список
            var rnd = new Random();
            var items = Enumerable.Range(0, soureCountRows)
                .Select(x => new T()
                {
                    Data = "T_New_" + rnd.Next(99999).ToString(),
                    DateAdd = DateTime.Now
                }).ToList();

            // 2. Добавим записи
            using (var context = new ControlPointDbContext())
            {
                items.ForEach(x =>
                {
                    context.Add(x);
                });

                return await context.SaveChangesAsync();
            }
        }
    }

    /// <summary>
    /// Класс для формирования страничных данных для любой таблицы
    /// </summary>
    [Service]
    public class ViewTableManager<T>: WindsorAbstract, IViewTableManager<T> where T: class, ITableData
    {
        private readonly ControlPointDbContext _context = new ControlPointDbContext();

        /// <summary>
        /// Получить все данные таблицы
        /// </summary>
        /// <returns></returns>
        private IQueryable<T> GetSimpleData()
        {
            DbSet<T> _dbSet = _context.Set<T>();
            return _dbSet.AsQueryable();
        }

        /// <summary>
        /// Получить данные по таблице
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public IViewTableModel<T> Index(int page = 1)
        {
            page = page <= 0 ? 1 : page;
            int pageSize = 3;
            IQueryable<T> source = GetSimpleData();
            var count = source.Count();
            var items = source.Skip((page - 1) * pageSize).Take(pageSize).ToList();


            // Находим и создаем класс наследник от интерфейса IViewTableModel
            // и generic T

            // Получаю список типов, которые имеют название ViewTable
            Type extEntity = this.GetType();
            IEnumerable<Type> list = Assembly.GetAssembly(extEntity)
                .GetTypes()
                .Where(x => x.FullName.IndexOf("ViewTable") >= 0
                        && x.IsClass);
                

            // Получить список нужных Generic классов 
            var findObjects = list
                    .Where(x => x.IsGenericType)
                    .Select(x => new
                    {
                        Name = x.GetGenericArguments().Select(y => y.BaseType.FullName).FirstOrDefault(),
                        Self = x
                    });

            // Получить нужный класс реализацию
            var itemObject = findObjects.Where(x => x.Name.IndexOf(typeof(T).FullName) >= 0)
                .Select(x => x.Self).FirstOrDefault();

            if (itemObject != null)
            {
                var genericType = itemObject.MakeGenericType(typeof(T));

                // Создаем экземпляр класса и выполняем действие
                dynamic item = Activator.CreateInstance(genericType) as IViewTableModel<T>;

                // Заполняем класс данными
                var result = (item as IViewTableModel<T>);
                result .Page = new PageManager(count, page, pageSize);
                result.Items = items;

                return result;
            }

            return null;
        }
    }

    #endregion

}
