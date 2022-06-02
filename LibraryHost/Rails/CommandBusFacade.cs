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

        public string Handle(CreateBook command)
        {
            return _commandBus.Handle(command);
        }

        public string Handle(DeleteBook command)
        {
            return _commandBus.Handle(command);
        }

        public string Handle(CreateCustomer command)
        {
            return _commandBus.Handle(command);
        }

        public string Handle(DeleteCustomer command)
        {
            return _commandBus.Handle(command);
        }

        public string Handle(CreateBorrow command)
        {
            return _commandBus.Handle(command);
        }

        public string Handle(ReturnBorrow command)
        {
            return _commandBus.Handle(command);
        }
    }
}
