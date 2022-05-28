using Domain;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Application;
using Autofac;

namespace LibraryHost
{
    public class CommandBus : ICommandBus
    {
        public string Handle<TCommand>(TCommand command) where TCommand : ICommand
        {
            var handlers = ContainerIoC.RegisterContainerBuilder().Build().BeginLifetimeScope()
                .Resolve<IEnumerable<ICommandHandler<TCommand>>>().ToList();

            if (handlers.Count == 1)
            {
                return handlers[0].Handle(command);
            }
            else if (handlers.Count == 0)
            {
                return "Polecenie nie istnieje";
            }

            return "Polecenia nie można wykonać";
        }
    }
}
