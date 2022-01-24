using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryService
{
    public interface IRepository<T> where T : class
    {
        void Add(T obj);
        void Remove(T obj);
        void Attach(T obj);
        IEnumerable<T> Get();   
    }
}
