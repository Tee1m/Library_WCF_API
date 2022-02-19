﻿using LibraryService;
using Autofac;
using Library.Infrastructure;
using AutoMapper;
using AutoMapper.Configuration;
using System.Collections.Generic;
using System;
using System.Linq;

namespace LibraryHost
{
    public class ContainerIoC : Module
    {
        public static ContainerBuilder RegisterContainerBuilder()
        {
            ContainerBuilder builder = new ContainerBuilder();

            var autoMapperProfiles = new List<Profile>() {
                (Profile)Activator.CreateInstance(typeof(BorrowMappingProfile)),
                (Profile)Activator.CreateInstance(typeof(BookMappingProfile)),
                (Profile)Activator.CreateInstance(typeof(CustomerMappingProfile))};

            builder.Register(context => new MapperConfiguration(cfg =>
            {
                foreach (var profile in autoMapperProfiles)
                {
                    cfg.AddProfile(profile);
                }
            }));

            builder.Register(context => context.Resolve<MapperConfiguration>()
                .CreateMapper()).As<IMapper>();
            builder.Register(context => new LibraryDb())
                .As<LibraryDb>();
            builder.Register(context => new UnitOfWork(context.Resolve<LibraryDb>(), context.Resolve<IMapper>()))
                .As<IUnitOfWork>().SingleInstance();

            builder.Register(context => new BooksRepository(context.Resolve<LibraryDb>(), context.Resolve<IMapper>()))
                .As<IBooksRepository>();
            builder.Register(context => new CustomersRepository(context.Resolve<LibraryDb>(), context.Resolve<IMapper>()))
                .As<ICustomersRepository>();
            builder.Register(context => new BorrowsRepository(context.Resolve<LibraryDb>(), context.Resolve<IMapper>()))
                .As<IBorrowsRepository>();

            builder.Register(context => new BooksService(context.Resolve<IUnitOfWork>()))
                .As<IBooksService>();
            builder.Register(context => new BorrowsService(context.Resolve<IUnitOfWork>()))
                .As<IBorrowsService>();
            builder.Register(context => new CustomersService(context.Resolve<IUnitOfWork>()))
                .As<ICustomersService>();

            return builder;
        }
    }
}
