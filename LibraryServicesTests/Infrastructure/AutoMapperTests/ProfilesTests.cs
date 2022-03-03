using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Infrastructure;
using AutoMapper;
using LibraryService;

namespace Infrastructure
{
    [TestClass]
    public class ProfilesTests
    {
        [TestMethod]
        [DataRow(typeof(BookMappingProfile))]
        [DataRow(typeof(BorrowMappingProfile))]
        [DataRow(typeof(CustomerMappingProfile))]
        public void MappingProfilesAreValid(Type profileType)
        {
            var configuration = new MapperConfiguration(cfg =>
             cfg.AddProfile(profileType));

            configuration.AssertConfigurationIsValid();
        }
    }
}
