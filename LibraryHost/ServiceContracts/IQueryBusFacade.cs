using System.Collections.Generic;
using System.ServiceModel;
using Application;

namespace LibraryHost
{
    [ServiceContract]
    public interface IQueryBusFacade
    {
        [OperationContract]
        IEnumerable<IDTO> Handle(IQuery query);
    }
}
