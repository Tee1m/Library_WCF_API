using Domain;
using System.Collections.Generic;

namespace Application
{
    public interface ICustomersRepository
    {
        void Add(Customer obj);
        void Remove(Customer obj);
        void Update(Customer obj);
        List<Customer> Get();

    }
}
