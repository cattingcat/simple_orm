using DataAccessor.ORM.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessor.ORM
{
    internal class OrmMap
    {
        private IDictionary<string, PropertyInfo> colNameToPropertyMap;

        public string TableName { get; set; }
        public string ID { get; set; }

        private OrmMap()
        {
            colNameToPropertyMap = new Dictionary<string, PropertyInfo>();
        }

        public PropertyInfo this[string columnName]
        {
            get
            {
                return colNameToPropertyMap[columnName];
            }
            set
            {
                colNameToPropertyMap[columnName] = value;
            }
        }
        public ICollection<string> Columns
        {
            get
            {
                return colNameToPropertyMap.Keys;
            }
        }
        public PropertyInfo GetIDPropertyInfo() 
        {
            return this[ID];
        }
        public System.Data.DbType GetDbType(string columnName)
        {
            ColumnAttribute attr = this[columnName].GetCustomAttribute<ColumnAttribute>();
            return attr.ColumnType;
        }

        public override string ToString()
        {
            StringBuilder b = new StringBuilder();
            b.Append("Table name: ");
            b.Append(TableName);
            b.Append("\nColumns: \n");
            foreach (var c in colNameToPropertyMap)
            {
                b.Append("  ");
                b.Append(c.Key);
                b.Append(" => ");
                b.Append(c.Value.Name);
                b.Append("\n");
            }
            return b.ToString();
        }

        public static OrmMap FromType(Type type)
        {
            TableAttribute attr = (TableAttribute)type.GetCustomAttributes(typeof(TableAttribute), true).SingleOrDefault();
            if (attr == null)
            {
                throw new Exception("this type have no TableAttribute");
            }
            OrmMap map = new OrmMap();
            map.TableName = attr.TableName;

            foreach (PropertyInfo p in type.GetProperties())
            {
                ColumnAttribute ca = (ColumnAttribute)p.GetCustomAttributes(typeof(ColumnAttribute), true).SingleOrDefault();
                if (ca != null)
                {
                    map[ca.ColumnName] = p;
                    if (ca is IdAttribute)
                    {
                        map.ID = ca.ColumnName;
                    }
                }
            }
            return map;
        }
    }
}
