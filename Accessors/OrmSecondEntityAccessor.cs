using DataAccessor.Entity;
using DataAccessor.ORM;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace DataAccessor.Accessors
{
    class OrmSecondEntityAccessor: IAccessor<SecondEntity>
    {
        private MyORM orm;

        public OrmSecondEntityAccessor()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SQLServerCE"].ConnectionString;
            string providerName = ConfigurationManager.ConnectionStrings["SQLServerCE"].ProviderName;
            DbProviderFactory factory = DbProviderFactories.GetFactory(providerName);

            orm = new MyORM(factory, connectionString, typeof(SecondEntity));
        }

        public ICollection<SecondEntity> GetAll()
        {
            return orm.SelectAll<SecondEntity>();
        }

        public SecondEntity GetById(object id)
        {
            return orm.SelectById<SecondEntity>(id);
        }

        public void DeleteById(object id)
        {
            orm.Delete<SecondEntity>(id);
        }

        public void Insert(SecondEntity p)
        {
            orm.Insert(p);
        }
    }
}
