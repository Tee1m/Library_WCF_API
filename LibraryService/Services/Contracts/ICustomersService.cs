using System.Collections.Generic;
using System.ServiceModel;

namespace LibraryService
{
    [ServiceContract]
    public interface ICustomersService
    {
        [OperationContract]
        string AddCustomer(CustomerDTO newCustomer);

        [OperationContract]
        string DeleteCustomer(int id);

        [OperationContract]
        List<CustomerDTO> GetCustomers();
    }
}
