using LibraryService;
using Autofac;

namespace LibraryHost
{
    public class DataBaseConnector
    {
        public static ContainerBuilder RegisterContainerBuilder()
        {
            ContainerBuilder builder = new ContainerBuilder();
           
            builder.Register(c => new LibraryDb())
                .As<IUnitOfWork>();

            builder.Register(c => new BooksRepository(c.Resolve<IUnitOfWork>()))
                .As<IBooksRepository>();
            builder.Register(c => new CustomersRepository(c.Resolve<IUnitOfWork>()))
                .As<ICustomersRepository>();
            builder.Register(c => new BorrowsRepository(c.Resolve<IUnitOfWork>()))
                .As<IBorrowsRepository>();

            builder.Register(c => new BooksService(c.Resolve<IBooksRepository>(), c.Resolve<IBorrowsRepository> ()))
                .As<IBooksService>();
            builder.Register(c => new BorrowsService(c.Resolve<ICustomersRepository>(), c.Resolve<IBorrowsRepository> (), c.Resolve<IBooksRepository>()))
                .As<IBorrowsService>();
            builder.Register(c => new CustomersService(c.Resolve<ICustomersRepository>(), c.Resolve<IBorrowsRepository> ()))
                .As<ICustomersService>();

            return builder;
        }
    }
}
