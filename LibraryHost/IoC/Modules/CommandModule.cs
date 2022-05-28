using Autofac;
using System;
using System.Linq;
using Domain;
using Application;
using System.Reflection;

namespace LibraryHost
{
    public class CommandModule : Autofac.Module 
    {
        private readonly Assembly[] _assembly;

        public CommandModule(Assembly[] assembly)
        {
            this._assembly = assembly;
        }

        protected override void Load(ContainerBuilder builder)
        {
            RegisterDomainServices(builder);

            builder.RegisterAssemblyTypes(_assembly)
                   .Where(x => x.IsAssignableTo<ICommandHandler>())
                   .AsImplementedInterfaces();

            builder.Register<Func<Type, ICommandHandler>>(c =>
            {
                var context = c.Resolve<IComponentContext>();

                return t =>
                {
                    var handlerType = typeof(ICommandHandler<>).MakeGenericType(t);
                    return (ICommandHandler)context.Resolve(handlerType);
                };
            });

            builder.Register(context => new CommandBus())
                .As<ICommandBus>();

            builder.Register(context => new CommandBusFacade(context.Resolve<ICommandBus>()))
                .As<ICommandBusFacade>();
        }

        private static void RegisterDomainServices(ContainerBuilder builder)
        {
            builder.Register(context => new BookUniquenessChecker(context.Resolve<IBooksRepository>()))
            .As<IBookUniquenessChecker>();

            builder.Register(context => new CustomerUniquenessChecker(context.Resolve<ICustomersRepository>()))
            .As<ICustomerUniquenessChecker>();
        }
    }
}
