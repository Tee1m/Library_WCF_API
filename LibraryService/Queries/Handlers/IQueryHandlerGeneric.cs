using System.Collections.Generic;

namespace Application
{
    public interface IQueryHandler<TQuery> : IQueryHandler where TQuery : IQuery
    {
        IEnumerable<IDTO> Handle(TQuery query);
    }
}
