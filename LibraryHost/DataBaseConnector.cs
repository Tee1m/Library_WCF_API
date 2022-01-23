using LibraryService;
using Autofac;

namespace LibraryHost
{
    public class DataBaseConnector
    {
        public static ContainerBuilder RegisterContainerBuilder()
        {
            ContainerBuilder builder = new ContainerBuilder();
            var dataBase = new LibraryDb();

            builder.Register(c => new LibraryDbClient(dataBase))
                .As<IDataBaseClient>();

            builder.Register(c => new BooksService(c.Resolve<IDataBaseClient>()))
                .As<IBooksService>();
            builder.Register(c => new BorrowsService(c.Resolve<IDataBaseClient>()))
                .As<IBorrowsService>();
            builder.Register(c => new CustomersService(c.Resolve<IDataBaseClient>()))
                .As<ICustomersService>();

            return builder;
        }
    }
}
