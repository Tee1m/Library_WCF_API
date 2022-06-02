using Application;
using Autofac;
using Library.Infrastructure;
using AutoMapper;
using AutoMapper.Configuration;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Configuration;
using Infrastructure;
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

            builder.Register(context => new CommandBus())
                .As<ICommandBus>();

            builder.Register(context => new CommandBusFacade(context.Resolve<ICommandBus>()))
                .As<ICommandBusFacade>();

            builder.Register(context => new QueryBus())
                   .As<IQueryBus>();

            builder.Register(context => new QueryBusFacade(context.Resolve<IQueryBus>()))
                   .As<IQueryBusFacade>();
            return builder;
        }
    }

}
