using Domain;
using System.Collections.Generic;

namespace Application
{
    public interface IBorrowsRepository
    {
        void Add(Customer customer, Book book);
        void Remove(Borrow obj);
        void Update(Borrow obj);
        List<Borrow> Get();

    }
}
