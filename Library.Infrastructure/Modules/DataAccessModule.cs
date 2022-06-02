using Autofac;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;
using Application;
using AutoMapper;
using System.Reflection;

namespace Infrastructure
{
    public class DataAccessModule : Autofac.Module
    {
        private readonly string _connectionString;
        private readonly Assembly[] _assembly;

        public DataAccessModule(string connectionString, Assembly[] assembly)
        {
            this._connectionString = connectionString;
            this._assembly = assembly;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(context => new LibraryDb(_connectionString))
            .As<LibraryDb>();

            RegisterAutoMapper(builder);

            builder.Register(context => new UnitOfWork(context.Resolve<LibraryDb>(), context.Resolve<IMapper>()))
                   .As<IUnitOfWork>().SingleInstance();

            builder.Register(context => new BooksRepository(context.Resolve<LibraryDb>(), context.Resolve<IMapper>()))
                   .As<IBooksRepository>();
            builder.Register(context => new CustomersRepository(context.Resolve<LibraryDb>(), context.Resolve<IMapper>()))
                   .As<ICustomersRepository>();
            builder.Register(context => new BorrowsRepository(context.Resolve<LibraryDb>(), context.Resolve<IMapper>()))
                   .As<IBorrowsRepository>();
        }

        private void RegisterAutoMapper(ContainerBuilder builder)
        {
            var autoMapperProfileTypes = _assembly.SelectMany(a => a.GetTypes().Where(p => typeof(Profile)
                .IsAssignableFrom(p) && p.IsPublic && !p.IsAbstract));

            builder.Register(ctx => new MapperConfiguration(cfg =>
            {
                foreach (var profile in autoMapperProfileTypes)
                {
                    cfg.AddProfile(profile);
                }
            }));

            builder.Register(context => context.Resolve<MapperConfiguration>()
                .CreateMapper()).As<IMapper>();
        }
    }
}
