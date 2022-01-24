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

            builder.Register(c => new DbClient<Book>(c.Resolve<IUnitOfWork>()))
                .As<IRepository<Book>>();
            builder.Register(c => new DbClient<Customer>(c.Resolve<IUnitOfWork>()))
                .As<IRepository<Customer>>();
            builder.Register(c => new DbClient<Borrow>(c.Resolve<IUnitOfWork>()))
                .As<IRepository<Borrow>>();

            builder.Register(c => new BooksService(c.Resolve<IRepository<Book>>(), c.Resolve<IRepository<Borrow>>()))
                .As<IBooksService>();
            builder.Register(c => new BorrowsService(c.Resolve<IRepository<Customer>>(), c.Resolve<IRepository<Borrow>>(), c.Resolve<IRepository<Book>>()))
                .As<IBorrowsService>();
            builder.Register(c => new CustomersService(c.Resolve<IRepository<Customer>>(), c.Resolve<IRepository<Borrow>>()))
                .As<ICustomersService>();

            return builder;
        }
    }
}
