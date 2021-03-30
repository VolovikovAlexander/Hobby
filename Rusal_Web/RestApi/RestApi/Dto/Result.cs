using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi.Dto
{
    public class Result
    {
        private int _id = -1;
        /// <summary>
        /// Код полученной записи
        /// </summary>
        public int ID { get => _id; set => _id = value; }

        /// <summary>
        /// Описание ошибки при вставке
        /// </summary>
        public string ErrorText { get; set; }
    }
}
