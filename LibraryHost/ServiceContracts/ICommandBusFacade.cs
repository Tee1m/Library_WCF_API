using System.ServiceModel;
using Application;

namespace LibraryHost
{
    [ServiceContract]
    public interface ICommandBusFacade
    {
        [OperationContract]
        string Handle(ICommand command);
    }
}
