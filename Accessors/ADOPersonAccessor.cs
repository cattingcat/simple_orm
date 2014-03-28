using DataAccessor.Entity;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;

namespace DataAccessor.Accessors
{
    class ADOPersonAccessor: IAccessor<Person>
    {
        private DbProviderFactory factory;
        private string connectionString;
        
        public ADOPersonAccessor()
        {
            connectionString = ConfigurationManager.ConnectionStrings["SQLServerCE"].ConnectionString;
            string providerName = ConfigurationManager.ConnectionStrings["SQLServerCE"].ProviderName;
            factory = DbProviderFactories.GetFactory(providerName);
        }
        public ICollection<Person> GetAll()
        {
            DbConnection conn = factory.CreateConnection();
            conn.ConnectionString = connectionString;
            conn.Open();
            using (conn)
            {
                DbCommand comm = conn.CreateCommand();
                comm.CommandText = "SELECT identificator, NameColumn, LastNameColumn from PersonTable";
                DbDataReader reader = comm.ExecuteReader();
                List<Person> result = new List<Person>();
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = string.Empty;
                    string lastName = string.Empty;

                    if(!reader.IsDBNull(1))
                        name = reader.GetString(1);

                    if(!reader.IsDBNull(2))
                        lastName = reader.GetString(2);

                    result.Add(new Person { ID = id, Name = name, LastName=lastName });
                }
                return result;
            }            
        }

        public Person GetById(object id)
        {
            DbConnection conn = factory.CreateConnection();
            conn.ConnectionString = connectionString;
            conn.Open();
            using (conn)
            {
                DbCommand comm = conn.CreateCommand();
                comm.CommandText = 
                    "SELECT identificator, NameColumn, LastNameColumn"+
                    " FROM PersonTable WHERE identificator=@id";
                DbParameter param = comm.CreateParameter();
                param.DbType = System.Data.DbType.Int32; 
                param.ParameterName="@id";
                param.Value = id;          
                comm.Parameters.Add(param);
                
                DbDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    int _id = reader.GetInt32(0);
                    string name = string.Empty;
                    string lastName = string.Empty;

                    if (!reader.IsDBNull(1))
                        name = reader.GetString(1);

                    if (!reader.IsDBNull(2))
                        lastName = reader.GetString(2);

                    return new Person { ID = _id, Name = name, LastName = lastName };
                }
            }
            return null;
        }

        public void DeleteById(object id)
        {
            DbConnection conn = factory.CreateConnection();
            conn.ConnectionString = connectionString;
            conn.Open();
            using (conn)
            {
                DbCommand comm = conn.CreateCommand();
                comm.CommandText =
                    "DELETE FROM PersonTable WHERE identificator=@id";
                DbParameter param = comm.CreateParameter();
                param.DbType = System.Data.DbType.Int32;
                param.ParameterName = "@id";
                param.Value = id;
                comm.Parameters.Add(param);
                comm.ExecuteNonQuery();
            }
        }

        public void Insert(Person p)
        {
            DbConnection conn = factory.CreateConnection();
            conn.ConnectionString = connectionString;
            conn.Open();
            using (conn)
            {
                DbCommand comm = conn.CreateCommand();
                comm.CommandText =
                    "INSERT INTO PersonTable(identificator, NameColumn, LastNameColumn)"+
                    " VALUES(@id, @name, @lastName)";
                DbParameter param = comm.CreateParameter();
                param.DbType = System.Data.DbType.Int32;
                param.ParameterName = "@id";
                param.Value = p.ID;
                comm.Parameters.Add(param);

                param = comm.CreateParameter();
                param.DbType = System.Data.DbType.String;
                param.ParameterName = "@name";
                param.Value = p.ID;
                comm.Parameters.Add(param);

                param = comm.CreateParameter();
                param.DbType = System.Data.DbType.String;
                param.ParameterName = "@lastName";
                param.Value = p.ID;
                comm.Parameters.Add(param);

                comm.ExecuteNonQuery();
            }
        }
    }
}
