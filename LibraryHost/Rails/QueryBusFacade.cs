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

        public List<BorrowDTO> Handle(GetBorrows query)
        {
            return _queryBus.Handle(query) as List<BorrowDTO>;
        }

        public List<CustomerDTO> Handle(GetCustomers query)
        {
            return _queryBus.Handle(query) as List<CustomerDTO>;
        }

        public List<BookDTO> Handle(GetBooks query)
        {
            return _queryBus.Handle(query) as List<BookDTO>;
        }
    }
}
