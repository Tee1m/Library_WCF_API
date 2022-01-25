using System.Collections.Generic;

namespace LibraryService
{
    public interface ICustomersRepository
    {
        void Add(Customer obj);
        void Remove(Customer obj);
        void Attach(Customer obj);
        List<Customer> Get();
    }
}
