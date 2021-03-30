using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi.Dto
{

    public class SelectColor
    {
        public int ColorID { get; set; }

        public bool Select { get; set; }
    }

    public class SelectDrink
    {
        public int DrinkID { get; set; }

        public bool Select { get; set; }
    }

    /// <summary>
    /// Реализация DTO по передаче данных о выборе пользователя
    /// </summary>
    public class MainForm_Select
    {
        public IEnumerable<SelectColor> SelectColors { get; set; }

        public IEnumerable<SelectDrink> SelectDrinks { get; set; }

        public int MainID { get; set; }
    }
}
