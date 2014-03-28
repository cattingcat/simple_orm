using DataAccessor.ORM.Attributes;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DataAccessor.ORM
{
    public class MyORM
    {
        private IDictionary<Type, OrmMap> mappingPool;
        private DbProviderFactory factory;
        private string connectionString;

        public MyORM(DbProviderFactory factory, string connectionString, params Type[] types)
        {
            mappingPool = new Dictionary<Type, OrmMap>();
            this.factory = factory;
            this.connectionString = connectionString;
            foreach (Type type in types)
            {
                RegisterType(type);
            }
        }

        public void RegisterType(Type type)
        {
            OrmMap map = null;
            if (!mappingPool.ContainsKey(type))
            {
                map = OrmMap.FromType(type);
                mappingPool[type] = map;
            }
            else
            {
                map = mappingPool[type];
            }
        }

        public ICollection<T> SelectAll<T>()
        {
            OrmMap map = mappingPool[typeof(T)];
            string selectQuery = map.BuildSelectAllQuery();
            List<T> result = new List<T>();

            using (DbConnection connection = GetOpenConnection())
            {
                DbCommand command = connection.CreateCommand();
                command.CommandText = map.BuildSelectAllQuery();

                DbDataReader reader = command.ExecuteReader();
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
            }
            return result;
        }
        public T SelectById<T>(object id) where T: new()
        {
            T result = new T();
            OrmMap map = mappingPool[typeof(T)];            
            string whereStatement = String.Format("{0}=@id", map.ID);
            string selectQuery = map.BuildSelectWhereQuery(whereStatement);        

            using (DbConnection connection = GetOpenConnection())
            {
                DbCommand command = connection.CreateCommand();
                command.CommandText = selectQuery;

                DbParameter param = command.CreateParameter();
                param.DbType = map.GetDbType(map.ID);
                param.ParameterName = "@id";
                param.Value = id;
                command.Parameters.Add(param);

                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; ++i)
                    {
                        string column = reader.GetName(i);
                        PropertyInfo info = map[column];
                        object value = reader.GetValue(i);
                        info.SetValue(result, value);
                    }
                }
            }
            return result;
        }
        public int Insert(object o)
        {
            Type type = o.GetType();
            OrmMap map = mappingPool[type];            
            StringBuilder argListBuilder = new StringBuilder();
            List<KeyValuePair<string, string>> colNameToNamedParam = new List<KeyValuePair<string, string>>();

            foreach (string col in map.Columns)
            {
                string namedParam = '@' + col;
                colNameToNamedParam.Add(new KeyValuePair<string, string>(col, namedParam));
                argListBuilder.Append(namedParam);
                argListBuilder.Append(',');
            }
            argListBuilder.Remove(argListBuilder.Length - 1, 1);

            string insertQuery = map.BuildInsertQuery(argListBuilder.ToString());

            using (DbConnection connection = GetOpenConnection())
            {
                DbCommand command = connection.CreateCommand();
                command.CommandText = insertQuery;
                foreach (var pair in colNameToNamedParam)
                {
                    DbParameter param = command.CreateParameter();
                    param.DbType = map.GetDbType(pair.Key);
                    param.ParameterName = pair.Value;
                    param.Value = map[pair.Key].GetValue(o);
                    command.Parameters.Add(param);
                }
                return command.ExecuteNonQuery();                
            }
        }
        public int Delete<T>(object id)
        {
            OrmMap map = mappingPool[typeof(T)];            
            string whereStatement = String.Format("{0}=@id", map.ID);

            string deleteQuery = map.BuildDeleteQuery(whereStatement);

            using (DbConnection connection = GetOpenConnection())
            {
                DbCommand command = connection.CreateCommand();
                command.CommandText = deleteQuery;

                DbParameter param = command.CreateParameter();
                param.DbType = map.GetDbType(map.ID);
                param.ParameterName = "@id";
                param.Value = id;
                command.Parameters.Add(param);

                return command.ExecuteNonQuery();
            }
        }

        private DbConnection GetOpenConnection()
        {
            DbConnection connection = factory.CreateConnection();
            connection.ConnectionString = connectionString;
            connection.Open();
            return connection;
        }
    }
}
