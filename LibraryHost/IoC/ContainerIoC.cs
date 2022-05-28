using Application;
using Autofac;
using Library.Infrastructure;
using AutoMapper;
using AutoMapper.Configuration;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Configuration;
using DAL;
using Domain;

namespace LibraryHost
{
    public class ContainerIoC : Module
    {
        public static ContainerBuilder RegisterContainerBuilder()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["LibraryDataBase"].ConnectionString;
            var assembly = AppDomain.CurrentDomain.GetAssemblies();

            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterModule(new DataAccessModule(connectionString, assembly));
            builder.RegisterModule(new CommandModule(assembly));
            builder.RegisterModule(new QueryModule(connectionString, assembly));

            return builder;
        }


        //public static ContainerBuilder RegisterContainerBuilder()
        //{
        //    ContainerBuilder builder = new ContainerBuilder();

        //    var connectionString = ConfigurationManager.ConnectionStrings["LibraryDataBase"].ConnectionString;

        //    builder.Register(context => new LibraryDb(connectionString))
        //        .As<LibraryDb>();

        //    RegisterAutoMapper(builder);
        //    RegisterDomainServices(builder);

        //    builder.Register(context => new UnitOfWork(context.Resolve<LibraryDb>(), context.Resolve<IMapper>()))
        //        .As<IUnitOfWork>().SingleInstance();

        //    builder.Register(context => new BooksRepository(context.Resolve<LibraryDb>(), context.Resolve<IMapper>()))
        //        .As<IBooksRepository>();
        //    builder.Register(context => new CustomersRepository(context.Resolve<LibraryDb>(), context.Resolve<IMapper>()))
        //        .As<ICustomersRepository>();
        //    builder.Register(context => new BorrowsRepository(context.Resolve<LibraryDb>(), context.Resolve<IMapper>()))
        //        .As<IBorrowsRepository>();

        //    //CommandsBus
        //    builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
        //    .Where(x => x.IsAssignableTo<ICommandHandler>())
        //    .AsImplementedInterfaces();

        //    builder.Register<Func<Type, ICommandHandler>>(c =>
        //    {
        //        var context = c.Resolve<IComponentContext>();

        //        return t =>
        //        {
        //            var handlerType = typeof(ICommandHandler<>).MakeGenericType(t);
        //            return (ICommandHandler)context.Resolve(handlerType);
        //        };
        //    });

        //    builder.Register(context => new CommandBus())
        //        .As<ICommandBus>();

        //    builder.Register(context => new CommandBusFacade(context.Resolve<ICommandBus>()))
        //        .As<ICommandBusFacade>();

        //    //End

        //    //QueryBus
        //    builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
        //    .Where(x => x.IsAssignableTo<IQueryHandler>())
        //    .AsImplementedInterfaces();

        //    builder.Register<Func<Type, IQueryHandler>>(c =>
        //    {
        //        var context = c.Resolve<IComponentContext>();

        //        return t =>
        //        {
        //            var handlerType = typeof(IQueryHandler<>).MakeGenericType(t);
        //            return (IQueryHandler)context.Resolve(handlerType);
        //        };
        //    });

        //    builder.Register(context => new QueryBus())
        //        .As<IQueryBus>();

        //    builder.Register(context => new SqlConnectionFactory(connectionString))
        //        .As<ISqlConnectionFactory>();

        //    builder.Register(context => new QueryBusFacade(context.Resolve<IQueryBus>()))
        //            .As<IQueryBusFacade>();

        //    //End



        //    return builder;
        //}

        //private static void RegisterQueryModule(ContainerBuilder builder)
        //{

        //}

        //private static void RegisterAutoMapper(ContainerBuilder builder)
        //{
        //    var autoMapperProfileTypes = AppDomain.CurrentDomain.GetAssemblies()
        //        .SelectMany(a => a.GetTypes().Where(p => typeof(Profile)
        //        .IsAssignableFrom(p) && p.IsPublic && !p.IsAbstract));

        //    builder.Register(ctx => new MapperConfiguration(cfg =>
        //    {
        //        foreach (var profile in autoMapperProfileTypes)
        //        {
        //            cfg.AddProfile(profile);
        //        }
        //    }));

        //    builder.Register(context => context.Resolve<MapperConfiguration>()
        //        .CreateMapper()).As<IMapper>();
        //}

        //private static void RegisterDomainServices(ContainerBuilder builder)
        //{
        //    builder.Register(context => new BookUniquenessChecker(context.Resolve<IBooksRepository>()))
        //    .As<IBookUniquenessChecker>();

        //    builder.Register(context => new CustomerUniquenessChecker(context.Resolve<ICustomersRepository>()))
        //    .As<ICustomerUniquenessChecker>();
        //}
    }

}
