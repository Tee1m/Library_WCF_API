using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryService
{
    public class DbClient<T> : IRepository<T> where T : class
    {
        private readonly IUnitOfWork _context;
        public DbClient(IUnitOfWork context)
        {
            this._context = context;
        }

        public void Add(T obj)
        {
            _context.Add<T>(obj);
            _context.Commit();
        }

        public void Remove(T obj)
        {
            _context.Remove<T>(obj);
            _context.Commit();
        }

        public IEnumerable<T> Get()
        {
            return _context.Get<T>().ToList<T>();
        }

        public void Attach(T obj)
        {
            _context.Attach<T>(obj);
        }
    }
}
