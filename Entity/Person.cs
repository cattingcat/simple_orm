using DataAccessor.ORM.Attributes;
using System;

namespace DataAccessor.Entity
{
    [Serializable]
    [Table(TableName = "PersonTable")]
    public class Person
    {

        [Id(ColumnName = "identificator", ColumnType=System.Data.DbType.Int32)]
        public int ID { get; set; }
        [Column(ColumnName = "NameColumn", ColumnType=System.Data.DbType.String)]
        public string Name { get; set; }
        [Column(ColumnName = "LastNameColumn", ColumnType=System.Data.DbType.String)]
        public string LastName { get; set; }

        public DateTime DayOfBirth { get; set; }

        public override string ToString()
        {
            return String.Format("id: {0}, name: {1}, lastname: {2}, dyaOfBirth: {3}", ID, Name, LastName, DayOfBirth);
        }
    }
}
