using Domain;
using System.Collections.Generic;
using System.ServiceModel;

namespace Application
{
    [ServiceContract]
    public interface ICustomersService
    {
        [OperationContract]
        string AddCustomer(Customer newCustomer);

        [OperationContract]
        string DeleteCustomer(int id);

        [OperationContract]
        List<Customer> GetCustomers();
    }
}
