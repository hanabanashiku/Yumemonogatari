using System.IO;
using System.Reflection;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using UnityEngine;

namespace Yumemonogatari.Data {
    public static class Database {
        private static ISessionFactory _factory;

        private static ISessionFactory Factory {
            get {
                if(_factory == null) {
                    var cfg = new Configuration();
                    cfg.DataBaseIntegration(x => {
                        x.ConnectionString = $"Data Source = {Path.Combine(Application.dataPath, "Resources", "data")}";
                        x.Driver<SQLite20Driver>();
                        x.Dialect<SQLiteDialect>();
                    });
                    cfg.AddAssembly(Assembly.GetExecutingAssembly());
                    _factory = cfg.BuildSessionFactory();
                }

                return _factory;
            }
        }

        public static ISession OpenSession() => Factory.OpenSession();

    }
}