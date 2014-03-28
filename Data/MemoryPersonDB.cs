using DataAccessor.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessor.Data
{
    public static class MemoryPersonDB
    {
        public static ICollection<Person> PersonDB;

        static MemoryPersonDB()
        {
            LinkedList<Person> tmp = new LinkedList<Person>();
            Random rand = new Random();
            for (int i = 0; i < 100; ++i)
            {
                tmp.AddFirst(new Person { 
                    ID = i, 
                    LastName = String.Format("{0} lastname", i.ToString()), 
                    Name = String.Format("{0} name", i.ToString()), 
                    DayOfBirth = DateTime.Today });
            }
            PersonDB = tmp;
        }
    }
}
