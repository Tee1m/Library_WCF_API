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
            var borrowsHost = new ServiceHost(typeof(BorrowsService));
            var customersHost = new ServiceHost(typeof(CustomersService));
            var booksHost = new ServiceHost(typeof(BooksService));

            var container = ContainerIoC.RegisterContainerBuilder().Build();
                         
            CheckServiceIsRegistrated(container, new TypedService(typeof(IBooksService)));
            CheckServiceIsRegistrated(container, new TypedService(typeof(IBorrowsService)));
            CheckServiceIsRegistrated(container, new TypedService(typeof(ICustomersService)));

            borrowsHost.AddDependencyInjectionBehavior<IBorrowsService>(container);
            customersHost.AddDependencyInjectionBehavior<ICustomersService>(container);
            booksHost.AddDependencyInjectionBehavior<IBooksService>(container);

            Console.WriteLine("Uruchamianie ...");

            borrowsHost.Opened += BorrowsServiceOpened;
            customersHost.Opened += CustomersServiceOpened;
            booksHost.Opened += BooksServiceOpened;

            borrowsHost.Open();
            customersHost.Open();
            booksHost.Open();

            Console.ReadKey();

            borrowsHost.Close();
            customersHost.Close();
            booksHost.Close();

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

        private static void BooksServiceOpened(object sender, EventArgs e)
        {
            Console.WriteLine("BooksRepository host został uruchomiony");
        }

        private static void CustomersServiceOpened(object sender, EventArgs e)
        {
            Console.WriteLine("CustomersRepository host został uruchomiony");
        }

        private static void BorrowsServiceOpened(object sender, EventArgs e)
        {
            Console.WriteLine("BorrowsRepository host został uruchomiony");
        }
    }
}
