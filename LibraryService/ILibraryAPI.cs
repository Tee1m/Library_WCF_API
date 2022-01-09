using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace LibraryService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface ILibraryAPI
    {
        
        [OperationContract]
        bool AddCustomer(Customer newCustomer);

        [OperationContract]
        bool DeleteCustomer(Customer Customer);
        
        [OperationContract]
        List<Customer> GetCustomers();


        // TODO: Add your service operations here
    }

}
