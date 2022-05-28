using Domain;
using System.Collections.Generic;
using System.ServiceModel;

namespace Application
{
    [ServiceContract]
    public interface ICommandBus
    {
        [OperationContract]
        string Handle<T>(T Command) where T : ICommand;
    }
}
