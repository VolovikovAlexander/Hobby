using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportService.Domain
{
    using System.ComponentModel.DataAnnotations;
    public class Employee
    {
        [Key]
        public string Name { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string  Inn { get; set; }

        [Required]
        public int Salary { get; set; }

        [Required]
        public string BuhCode { get; set; }

        public override string ToString()
        {
            // Replaced standart output for creating report
            var result = $"{Name}, {Salary}р.";
            return result;
        }
    }
}
