using System;
using System.Collections.Generic;
using System.Text;
using LinqToDB;
using LinqToDB.Mapping;
using Newtonsoft.Json.Serialization;

namespace RestApi
{
    /// <summary>
    /// Базовый интерфейс к любой таблице
    /// </summary>
    public interface IDBBase
    {
        public int ID { get; set; }

        public DateTime DateAdd { get; set; }
    }

    /// <summary>
    /// Реализация основного класса
    /// </summary>
    public class dbBase : IDBBase
    {
        private int _Id = 0;
        private DateTime _dateAdd = DateTime.Now;
        [PrimaryKey]
        [Column(Name ="ID"),PrimaryKey, Identity, NotNull, SkipValuesOnInsert]
        public int ID { get => _Id; set => _Id = value; }

        [Column(Name = "DateAdd"), NotNull]
        public DateTime DateAdd { get => _dateAdd; set => _dateAdd = value; }
    }

    /// <summary>
    /// Класс описание цветов
    /// </summary>
    [Table(Name = "refColors")]
    public class dbRefColors: dbBase
    {
        [Column(Name = "Description")]
        public string Description { get; set; }
    }

    /// <summary>
    /// Класс описание напитков
    /// </summary>
    [Table(Name = "refDrinks")]
    public class dbRefDrinks: dbBase
    {
        [Column(Name = "Description")]
        public string Description { get; set; }
    }

    /// <summary>
    /// Класс описание формы ввода (анкета)
    /// </summary>
    [Table(Name = "tblMainForm")]
    public class dbTblMainForm : dbBase
    {
        #region "Конструкторы"

        public dbTblMainForm() { }
        
        /// <summary>
        /// Конструктор копии данных через DTO
        /// </summary>
        /// <param name="sourceMainForm"></param>
        public dbTblMainForm(Dto.MainForm sourceMainForm)
        {
            if (sourceMainForm == null) return;
            LastName = sourceMainForm.LastName.Trim();
            FirstName = sourceMainForm.FirstName.Trim() ;
            // TODO: Тут можно проверку корректности сделать
            Birthday = sourceMainForm.Birthday;
            Phone = sourceMainForm.Phone.Trim();
        }

        #endregion

        /// <summary>
        /// Имя
        /// </summary>
        [Column(Name = "FirstName"), NotNull]
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        [Column(Name = "LastName"), NotNull]
        public string LastName { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        [Column(Name = "Birthday")]
        public DateTime Birthday { get; set; }

        /// <summary>
        /// Телефон
        /// </summary>
        [Column(Name = "Phone")]
        public string Phone { get; set; }
    }

    /// <summary>
    /// Класс описание таблицы связки для хранения информации многие ко многим
    /// </summary>
    [Table(Name = "tblMainForm_SelectionResult")]
    public class dbTblMainFormSelectionResult : dbBase
    {

        /// <summary>
        /// Код анкеты
        /// </summary>
        [Column(Name = "MainID")]
        public int MainID { get; set; }

        /// <summary>
        /// Код выбранного цвета
        /// </summary>
        [Column(Name = "ColorID")]
        public int? ColorID {get; set;}

        /// <summary>
        /// Код выбранного напитка
        /// </summary>
        [Column(Name = "DrinkID")]
        public int? DrinkID { get; set; }
    }

    /// <summary>
    /// Общий контекст для работы с базой дангных
    /// </summary>
    public class DbContext: LinqToDB.Data.DataConnection
    {
        
        public DbContext() : base("RusalWebSystem") { }

        /// <summary>
        /// Цвета
        /// </summary>
        public ITable<dbRefColors> Colors => GetTable<dbRefColors>();

        /// <summary>
        /// Напитки
        /// </summary>
        public ITable<dbRefDrinks> Drinks => GetTable<dbRefDrinks>();

        /// <summary>
        /// Анкеты
        /// </summary>
        public ITable<dbTblMainForm> MainForm => GetTable<dbTblMainForm>();

        /// <summary>
        /// Выбор вариантов в анкете
        /// </summary>
        public ITable<dbTblMainFormSelectionResult> MainFormSelectionResult => GetTable<dbTblMainFormSelectionResult>();
    }
}
