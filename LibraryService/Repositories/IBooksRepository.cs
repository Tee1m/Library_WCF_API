using Domain;
using System.Collections.Generic;

namespace Application
{
    public interface IBooksRepository
    {
        void Add(Book obj);
        void Remove(Book obj);
        void Update(Book obj);
        List<Book> Get();
    }
}
