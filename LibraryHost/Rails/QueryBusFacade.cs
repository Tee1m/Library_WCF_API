using System.Collections.Generic;
using System.ServiceModel;
using Application;

namespace LibraryHost
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class QueryBusFacade : IQueryBusFacade
    {
        private readonly IQueryBus _queryBus;
        public QueryBusFacade(IQueryBus queryBus)
        {
            this._queryBus = queryBus;
        }

        public IEnumerable<IDTO> Handle(IQuery query)
        {
            return _queryBus.Handle(query);
        }
    }
}
