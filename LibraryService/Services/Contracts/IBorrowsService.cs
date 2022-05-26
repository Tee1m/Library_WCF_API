using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Application
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IBorrowsService
    {          
        [OperationContract]
        string AddBorrow(int customerId, int bookId);

        [OperationContract]
        string ReturnBorrow(int id);

        [OperationContract]
        List<Borrow> GetBorrows();

        // TODO: Add your service operations here
    }

}
