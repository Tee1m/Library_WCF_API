using System.ServiceModel;
using Application;

namespace LibraryHost
{
    [ServiceContract]
    public interface ICommandBusFacade
    {
        [OperationContract(Name = "CreateBook")]
        string Handle(CreateBook command);

        [OperationContract(Name = "DeleteBook")]
        string Handle(DeleteBook command);

        [OperationContract(Name = "CreateCustomer")]
        string Handle(CreateCustomer command);

        [OperationContract(Name = "DeleteCustomer")]
        string Handle(DeleteCustomer command);

        [OperationContract(Name = "CreateBorrow")]
        string Handle(CreateBorrow command);

        [OperationContract(Name = "ReturnBorrow")]
        string Handle(ReturnBorrow command);
    }
}
