using System.Collections.Generic;
using System.ServiceModel;
using Application;

namespace LibraryHost
{
    [ServiceContract]
    public interface IQueryBusFacade
    {
        [OperationContract(Name = "GetBorrows")]
        List<BorrowDTO> Handle(GetBorrows query);

        [OperationContract(Name = "GetCustomers")]
        List<CustomerDTO> Handle(GetCustomers query);

        [OperationContract(Name = "GetBooks")]
        List<BookDTO> Handle(GetBooks query);
    }
}
