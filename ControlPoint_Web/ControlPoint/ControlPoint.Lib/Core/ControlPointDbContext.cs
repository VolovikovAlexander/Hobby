using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ControlPoint.Core
{
    using ControlPoint.Models;

    /// <summary>
    /// Контекст базы данных - MS SQL
    /// </summary>
    public class ControlPointDbContext: DbContext
    {

        /// <summary>
        /// Подключаем базу данных. Тут строку соединения поменять!
        /// </summary>
        /// <param name="x"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder x)
         => x.UseSqlServer(@"Data Source=localhost\sqlexpress;Initial Catalog=ControlPoint;User Id=sa;Password=123456;Pooling=false;Connect Timeout=500;");

        public DbSet<Table1> Table1 { get; set; }

        public DbSet<Table2> Table2 { get; set; }

        public DbSet<Table3> Table3 { get; set; }
    }
}
