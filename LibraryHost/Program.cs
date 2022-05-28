using Application;
using System;
using System.ServiceModel;
using Autofac;
using Autofac.Integration.Wcf;
using Autofac.Core;

namespace LibraryHost
{
    public class Program
    {
        static void Main(string[] args)
        {
            var commandBusFacade = new ServiceHost(typeof(CommandBusFacade));
            var queryBusFacade = new ServiceHost(typeof(QueryBusFacade));

            var container = ContainerIoC.RegisterContainerBuilder().Build();
                         
            CheckServiceIsRegistrated(container, new TypedService(typeof(IQueryBusFacade)));
            CheckServiceIsRegistrated(container, new TypedService(typeof(ICommandBusFacade)));

            commandBusFacade.AddDependencyInjectionBehavior<ICommandBusFacade>(container);
            queryBusFacade.AddDependencyInjectionBehavior<IQueryBusFacade>(container);

            Console.WriteLine("Uruchamianie ...");

            commandBusFacade.Opened += CommandBusServiceOpened;
            queryBusFacade.Opened += QueryBusServiceOpened;

            commandBusFacade.Open();
            queryBusFacade.Open();

            Console.ReadKey();

            commandBusFacade.Close();
            queryBusFacade.Close();

            Environment.Exit(0);
        }

        private static void CheckServiceIsRegistrated(IContainer container, Service service)
        {
            IComponentRegistration registration;
            if (!container.ComponentRegistry.TryGetRegistration(service, out registration))
            {
                Console.WriteLine($"The service typeof {service} has not been registered in the container.");
                Console.ReadLine();
                Environment.Exit(-1);
            }
        }

        private static void QueryBusServiceOpened(object sender, EventArgs e)
        {
            Console.WriteLine("QueryBusService host został uruchomiony");
        }

        private static void CommandBusServiceOpened(object sender, EventArgs e)
        {
            Console.WriteLine("CommandBusService host został uruchomiony");
        }
    }
}
