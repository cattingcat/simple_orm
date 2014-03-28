using DataAccessor.ORM.Attributes;
using System;
using System.Data;

namespace DataAccessor.Entity
{
    [Table(TableName = "SimpleTable")]
    class SecondEntity
    {
        [Id(ColumnName = "Id", ColumnType = DbType.String)]
        public string Field { get; set; }

        [Column(ColumnName = "NameColumn", ColumnType = DbType.String)]
        public string UnattributeField { get; set; }

        [Column(ColumnName = "Date", ColumnType = DbType.Date)]
        public DateTime? Date { get; set; }

        public override string ToString()
        {
            return String.Format("ID: {0}, Value: {1}, Date: {2}", Field, UnattributeField, (Date == null ? "null" : Date.ToString()));
        }
    }
}
