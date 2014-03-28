using DataAccessor.Accessors;
using DataAccessor.Entity;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessor.Tests
{
    [TestFixture]
    class OrmTest
    {
        [Test]
        public void GetNotExistId()
        {
            IAccessor<Person> acc = new OrmPersonAccessor();
            var p = acc.GetById(9999);
            Assert.AreEqual(null, p);
        }

        [Test]
        public void DeleteNotExistId()
        {
            IAccessor<Person> acc = new OrmPersonAccessor();
            acc.DeleteById(9999);
        }

        [Test]
        public void InsertExistId()
        {
            IAccessor<Person> acc = new OrmPersonAccessor();
            acc.Insert(new Person { ID = 1 });
        }
    }
}
