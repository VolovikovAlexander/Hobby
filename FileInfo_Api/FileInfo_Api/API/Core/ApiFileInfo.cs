using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;

namespace API.Core
{
    /////////////////////////////////////////////////////////////////
    // Реализация классов

    /// <summary>
    /// Информация о файле. Передается для обработки
    /// </summary>
    public class ApiFileInfo : IApiFileInfo
    {
        protected DelimetrType _type = DelimetrType.Comma;
        protected string _body = "";

        public string Name { get; set;}
        public string Body { get => _body.Trim(); set { _body = value.Trim().Replace("\r", ""); }}
        public DelimetrType type { get => _type; set { _type = value; } }
    }

    /// <summary>
    /// Класс описание одной записи таблицы
    /// </summary>
    public class ApiTableRowInfo : IApiTableRowInfo
    {
        private List<object> _values = new List<object>();
        public List<object> values { get => _values; set { values = _values; } }

        #region "Конструкторы"
        public ApiTableRowInfo()
        { }

        public ApiTableRowInfo(string[] localItems)
        {
            _values.Clear();
            _values.AddRange(localItems);
        }

        #endregion

        public override string ToString()
        {
            string result = string.Join("|", _values.Select(x => (string)x).ToArray());
            return result.Trim();
        }
    }

    /// <summary>
    /// Реализация основного класса
    /// </summary>
    public abstract  class Common : ICommon
    {
        protected string _errorText = "";
        public string ErrorText { get => _errorText.Trim();  set { _errorText = value.Trim(); } }

        bool IsError { get => string.IsNullOrEmpty(ErrorText) ? false : true; }

        static string GetTab() { return Convert.ToChar(9).ToString(); }
    }



    /// <summary>
    /// Класс реализация получение данных из текстового файла
    /// </summary>
    public class ApiTableInfo : Common, IApiTableInfo
    {
        protected DelimetrType _type = DelimetrType.Comma;
        protected ApiFileInfo _file = null;
        protected List<ApiTableRowInfo> _rows = new List<ApiTableRowInfo>();
        private List<string> _columns = new List<string>();


        #region "Конструкторы"

        public ApiTableInfo() { }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="localFile">Файл, загруженный через API</param>
        /// <param name="localType">Тип разделителя</param>
        public ApiTableInfo(ApiFileInfo localFile)
        {
            _file = localFile;
            _type = localFile.type;

            // Сразу же сформируем структуру
            if (_file != null)
                if (!string.IsNullOrEmpty(_file.Body))
                    Build();
        }

        #endregion

        #region "Реализация интерфейса IApiTableInfo"

        public List<string> columns { get => _columns; set { _columns = value; } }
        public ApiFileInfo file { get => _file; set { _file = value; } }
        public List<ApiTableRowInfo> rows { get => _rows; set { _rows = value; } }

        #endregion

        /// <summary>
        /// Обработать текстовый документ и сформировать структуру
        /// </summary>
        public void Build()
        {
            if (_file == null) return;
            if (string.IsNullOrEmpty(_file.Body)) return;

            var items = _file.Body.Split('\n');
            if(items.Count() == 0)
            {
                _errorText = $"Текст файла {_file.Name} не корректный. Нет строк с данными!";
                return;
            }

            // Получить разделитель
            var attribs = (DelimiterAttribute[])_type.GetType().GetField(_type.ToString()).GetCustomAttributes(typeof(DelimiterAttribute), true);
            // TODO: * -> Tab заменяю, т.к. не нашел возможности в аттрибуты Tab включить
            var charDelimiter = attribs.Length > 0 ? attribs[0].Name.Replace("***","\t") : _type.ToString();
            if(string.IsNullOrEmpty(charDelimiter))
            {
                _errorText = "Не корректо передан параметры - разделитель!";
                return;
            }

            foreach(var item in items)
            {
                var values = item.Split( charDelimiter.ToCharArray().FirstOrDefault());
                if(values.Count() <= 1 )
                {
                    _errorText = $"Не корректный разделитель выбран. Нет данных!";
                    return;
                }

                var trimValues = values.Select(x => x.Trim());
                var row = new ApiTableRowInfo(trimValues.ToArray());
                _rows.Add(row);
            }

            // Первая строка - это названия колонок
            var firstRow = _rows.First();
            _rows.Remove(firstRow);

            // Получаем колонки
            _columns = firstRow.values.Select(x => (string)x).ToList();

        }

        public override string ToString()
        {
            string result = "";
            if (_columns.Count() > 0)
                result = string.Join("|", _columns.ToArray()) + Environment.NewLine;

            result += "=================================================" + Environment.NewLine;
            foreach (var item in _rows)
                result += item.ToString() + Environment.NewLine;

            result += "=================================================" + Environment.NewLine;
            return result;

        }

        // the end
    }
}
