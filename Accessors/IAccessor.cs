using DataAccessor.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessor.Accessors
{
    interface IAccessor<T>
    {
        ICollection<T> GetAll();
        T GetById(object id);
        void DeleteById(object id);
        void Insert(T p);
    }
}
