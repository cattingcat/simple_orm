using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessor.ORM
{
    class DbReaderAdapter
    {
        private DbDataReader reader;
        private OrmMap map;

        public DbReaderAdapter(DbDataReader reader, OrmMap map)
        {
            this.reader = reader;
            this.map = map;
        }

        public T GetSingleResult<T>() where T: class, new()
        {
            while (reader.Read())
            {
                T o = new T();
                for (int i = 0; i < reader.FieldCount; ++i)
                {                  
                    string column = reader.GetName(i);
                    PropertyInfo info = map[column];
                    object value = reader.GetValue(i);
                    if (value != DBNull.Value)
                    {
                        info.SetValue(o, value);
                    }
                    else
                    {
                        info.SetValue(o, null);
                    }                    
                }
                return o;
            }
            return null;
        }

        public ICollection<T> GetMultipleResult<T>() where T : class, new()
        {
            List<T> result = new List<T>();
            while (reader.Read())
            {
                T o = Activator.CreateInstance<T>();
                for (int i = 0; i < reader.FieldCount; ++i)
                {
                    string column = reader.GetName(i);
                    PropertyInfo info = map[column];
                    object value = reader.GetValue(i);
                    if (value != DBNull.Value)
                    {
                        info.SetValue(o, value);
                    }
                    else
                    {
                        info.SetValue(o, null);
                    }
                }
                result.Add(o);
            }
            return result;
        }
    }
}
