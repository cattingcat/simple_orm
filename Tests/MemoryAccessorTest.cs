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
    class MemoryAccessorTest
    {
        [Test]
        public void GetNotExistId()
        {
            MemoryPersonAccessor acc = new MemoryPersonAccessor();            
            var p = acc.GetById(9999);
            Assert.AreEqual(null, p);
            
        }

        [Test]
        public void DeleteNotExistId()
        {
            MemoryPersonAccessor acc = new MemoryPersonAccessor();
            acc.DeleteById(9999);
        }

        [Test]
        public void InsertExistId()
        {
            MemoryPersonAccessor acc = new MemoryPersonAccessor();
            acc.Insert(new Person { ID = 1 });
        }
    }
}
