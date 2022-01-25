using System.Collections.Generic;

namespace LibraryService
{
    public interface IBooksRepository
    {
        void Add(Book obj);
        void Remove(Book obj);
        void Attach(Book obj);
        List<Book> Get();
    }
}
