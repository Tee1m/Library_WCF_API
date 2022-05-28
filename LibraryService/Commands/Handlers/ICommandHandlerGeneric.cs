namespace Application
{
    public interface ICommandHandler<TCommand> : ICommandHandler
        where TCommand : ICommand
    {
        string Handle(TCommand command);
    }
}
