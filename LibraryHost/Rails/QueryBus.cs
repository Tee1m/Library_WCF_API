using System.Collections.Generic;
using System.Linq;
using Application;
using Autofac;

namespace LibraryHost
{
    public class QueryBus : IQueryBus
    {
        public IEnumerable<IDTO> Handle<TQuery>(TQuery query) where TQuery : IQuery
        {
            var handlers = ContainerIoC.RegisterContainerBuilder().Build().BeginLifetimeScope()
            .Resolve<IEnumerable<IQueryHandler<TQuery>>>().ToList();

            if (handlers.Count == 1)
            {
                return handlers[0].Handle(query);
            }

            return new List<IDTO>();
        }
    }
}
