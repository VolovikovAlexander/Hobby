using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;

namespace RestApi.Dto
{
    /// <summary>
    /// DTO: Класс для передачи данных в таблицу администратора
    /// </summary>
    public class MainFormTablePreview
    {
        public int MainID { get; set; }

        public int ColorID { get; set; }

        public int DrinkID { get; set; }

        /// <summary>
        /// Наименование цвета
        /// </summary>
        public string ColorName { get; set; }

        /// <summary>
        /// Наименование напитка
        /// </summary>
        public string DrinkName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime Birthday { get; set; }

        /// <summary>
        /// Строковое представление даты рождения
        /// </summary>
        public string BirthdayString { get; set; }

        public string Phone { get; set; }

        #region "Статические вызовы"

        /// <summary>
        /// Получить представление данных
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<MainFormTablePreview> ToList()
        {
            using (var context = new DbContext())
            {
                var view =  (
                                 from s in context.MainForm
                                 join p in context.MainFormSelectionResult on s.ID equals p.MainID into g
                                 from t in g.DefaultIfEmpty()
                                 select new { s.ID, s.FirstName, s.LastName, s.Birthday, ColorID = t.ColorID ?? 0, DrinkID = t.DrinkID ?? 0, s.Phone }
                             );

                var result = view
                    .Select(x => new MainFormTablePreview()
                    {
                        MainID = x.ID,
                        DrinkID = x.DrinkID,
                        ColorID = x.ColorID,
                        LastName = x.LastName,
                        FirstName = x.FirstName,
                        Birthday = x.Birthday,
                        // Поле дата рождения соответствовать маске dd.mm.yyyy.
                        BirthdayString = x.Birthday.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture),
                        ColorName = context.Colors.Where(y => y.ID == x.ColorID).FirstOrDefault().Description ?? "",
                        DrinkName = context.Drinks.Where(y => y.ID == x.DrinkID).FirstOrDefault().Description ?? "",
                        Phone = x.Phone
                    });

                return result.ToList();
            }
        }

        #endregion
    }
}
