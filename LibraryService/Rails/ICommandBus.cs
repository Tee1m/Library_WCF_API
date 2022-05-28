using Domain;
using System.Collections.Generic;

namespace Application
{

    public interface ICommandBus
    {
        
        string Handle<T>(T command) where T : ICommand;
    }
}
