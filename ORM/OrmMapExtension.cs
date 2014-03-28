using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessor.ORM
{
    static class OrmMapExtension
    {
        public static string MapArgColumnList(this OrmMap map)
        {
            StringBuilder b = new StringBuilder();
            foreach (string col in map.Columns)
            {
                b.Append(col);
                b.Append(',');
            }
            b.Remove(b.Length - 1, 1);
            return b.ToString();
        }
        public static string BuildSelectAllQuery(this OrmMap map)
        {
            string selectTemplate = "SELECT {0} FROM {1}";
            string argCols = MapArgColumnList(map);
            return String.Format(selectTemplate, argCols, map.TableName);
        }
        public static string BuildSelectWhereQuery(this OrmMap map, string whereSection)
        {
            string selectTemplate = "SELECT {0} FROM {1} WHERE " + whereSection;
            string argCols = MapArgColumnList(map);
            return String.Format(selectTemplate, argCols, map.TableName);
        }
        public static string BuildInsertQuery(this OrmMap map, string argValues)
        {
            string insertTemplate = "INSERT INTO {0}({1}) VALUES(" + argValues + ")";
            string argCols = MapArgColumnList(map);
            return String.Format(insertTemplate, map.TableName, argCols);
        }
        public static string BuildDeleteQuery(this OrmMap map, string whereSection)
        {
            string deleteTemplate = "DELETE FROM {0} WHERE " + whereSection;
            return String.Format(deleteTemplate, map.TableName);
        }
    }
}
