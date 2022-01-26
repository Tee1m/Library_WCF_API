using System.Collections.Generic;

namespace LibraryService
{
    public interface IBooksRepository
    {
        void Add(BookDTO obj);
        void Remove(BookDTO obj);
        void Attach(BookDTO obj);
        List<BookDTO> Get();
    }
}
