using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        void Add<T>(T obj) where T : class;
        void Remove<T>(T obj) where T : class;
        void Attach<T>(T obj) where T : class;
        IQueryable<T> Get<T>() where T : class;
    }
}
