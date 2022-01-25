using System.Collections.Generic;

namespace LibraryService
{
    public interface IBorrowsRepository
    {
        void Add(Borrow obj);
        void Remove(Borrow obj);
        void Attach(Borrow obj);
        List<Borrow> Get();
    }
}
