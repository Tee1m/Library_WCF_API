using Domain;
using System.Collections.Generic;

namespace Application
{
    public interface IQueryBus
    {
        IEnumerable<IDTO> Handle<T>(T query) where T : IQuery;
    }
}
