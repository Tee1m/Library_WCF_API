using System.Collections.Generic;

namespace LibraryService
{
    public interface ICustomersRepository
    {
        void Add(CustomerDTO obj);
        void Remove(CustomerDTO obj);
        void Attach(CustomerDTO obj);
        List<CustomerDTO> Get();
    }
}
