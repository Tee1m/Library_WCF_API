using LibraryService;
using Autofac;
using Library.Infrastructure;
using AutoMapper;

namespace LibraryHost
{
    public class ContainerIoC
    {
        public static ContainerBuilder RegisterContainerBuilder()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.Register(c => new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile())))
                .As<IConfigurationProvider>();
            builder.Register(c => new Mapper(c.Resolve<IConfigurationProvider>()))
                .As<IMapper>();

            builder.Register(c => new LibraryDb())
                .As<IUnitOfWork>();

            builder.Register(c => new BooksRepository(c.Resolve<IUnitOfWork>(), c.Resolve<IMapper>()))
                .As<IBooksRepository>();
            builder.Register(c => new CustomersRepository(c.Resolve<IUnitOfWork>(), c.Resolve<IMapper>()))
                .As<ICustomersRepository>();
            builder.Register(c => new BorrowsRepository(c.Resolve<IUnitOfWork>(), c.Resolve<IMapper>()))
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
