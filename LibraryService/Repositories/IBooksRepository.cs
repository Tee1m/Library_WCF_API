using System.Collections.Generic;

namespace LibraryService
{
    public interface IBooksRepository
    {
        void Add(BookDTO obj);
        void Remove(BookDTO obj);
        void Update(BookDTO obj);
        List<BookDTO> Get();
    }
}
