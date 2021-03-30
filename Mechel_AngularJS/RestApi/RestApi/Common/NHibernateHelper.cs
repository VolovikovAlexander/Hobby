using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi
{
    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;
    using NHibernate;
    using NHibernate.Tool.hbm2ddl;

    public class NHibernateHelper<T> where T: IBaseEntity
    {
        public static ISession OpenSession() 
        {
            // Строка соединения с базой данных DataQueue
            // Каталог запуска приложения

            var localCS = String.Format("Data Source={0}", @"C:\Projects\Hobby\Mechel_AngularJS\DataQueue.db");
            /////////////////////////////////////////////////
            
            ISessionFactory sessionFactory = Fluently.Configure()
                    .Database(SQLiteConfiguration.Standard.ConnectionString(localCS)
                    .ShowSql()
            )
            .Mappings(m => m.FluentMappings.AddFromAssemblyOf<T>())
            .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true))
            .BuildSessionFactory();
            return sessionFactory.OpenSession();
        }
    }
}
