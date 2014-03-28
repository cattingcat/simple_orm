using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using DataAccessor.Accessors;
using DataAccessor.Entity;

namespace DataAccessor.Tests
{
    [TestFixture]
    class FileAccessorTest
    {
        [Test]
        public void GetNotExistId()
        {
            IAccessor<Person> acc = new FilePersonAccessor();
            var p = acc.GetById(9999);
            Assert.AreEqual(null, p);
        }

        [Test]
        public void DeleteNotExistId()
        {
            IAccessor<Person> acc = new FilePersonAccessor();
            acc.DeleteById(9999);
        }

        [Test]
        public void InsertExistId()
        {
            IAccessor<Person> acc = new FilePersonAccessor();
            acc.Insert(new Person { ID = 1 });
        }
    }
}
