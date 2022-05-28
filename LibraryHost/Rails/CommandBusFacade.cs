using System.ServiceModel;
using Application;

namespace LibraryHost
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class CommandBusFacade : ICommandBusFacade
    {
        private readonly ICommandBus _commandBus;
        public CommandBusFacade(ICommandBus commandBus)
        {
            this._commandBus = commandBus;
        }

        public string Handle(ICommand command)
        {
           return _commandBus.Handle(command);
        }
    }
}
