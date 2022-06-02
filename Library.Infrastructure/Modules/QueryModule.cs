using Autofac;
using System;
using System.Linq;
using Infrastructure;
using Application;
using System.Reflection;

namespace Infrastructure
{
    public class QueryModule : Autofac.Module
    {
        private readonly Assembly[] _assembly;
        private readonly string _connectionString;
        public QueryModule(string connectionString, Assembly[] assembly)
        {
            this._assembly = assembly;
            this._connectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(_assembly)
                   .Where(x => x.IsAssignableTo<IQueryHandler>())
                   .AsImplementedInterfaces();

            builder.Register<Func<Type, IQueryHandler>>(c =>
            {
                var context = c.Resolve<IComponentContext>();

                return t =>
                {
                    var handlerType = typeof(IQueryHandler<>).MakeGenericType(t);
                    return (IQueryHandler)context.Resolve(handlerType);
                };
            });

            builder.Register(context => new SqlConnectionFactory(_connectionString))
                .As<ISqlConnectionFactory>();
        }
    }
}
