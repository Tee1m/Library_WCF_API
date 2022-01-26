using System.Collections.Generic;

namespace LibraryService
{
    public interface IBorrowsRepository
    {
        void Add(CustomerDTO customer, BookDTO book);
        void Remove(BorrowDTO obj);
        void Attach(BorrowDTO obj);
        List<BorrowDTO> Get();
    }
}
