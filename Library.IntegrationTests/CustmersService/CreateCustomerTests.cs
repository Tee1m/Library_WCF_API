using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryHost;
using Application;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Autofac.Integration.Wcf;
using System;
using System.ServiceModel.Web;
using Autofac;
using System.Linq;
using System.ServiceModel.Description;
using System.Transactions;
using Library.IntegrationTests;
using Domain;

namespace CustomerCommandTests
{
    [TestClass]
    public class CreateCustomerTests : TransactionIsolator
    {
        private ICommandBus commandBus = new CommandBus();
        private IQueryBus queryBus = new QueryBus();
        private CreateCustomer command = new CreateCustomer();

        [TestMethod]
        public void AddNewCustomer()
        {
            //when 
            command.Name = "Maciej";
            command.Surname = "Hanulak";
            command.Address = "ul. Moja";
            command.TelephoneNumber = "123456789";

            //given
            var throwed = commandBus.Handle(command);

            //then
            StringAssert.Contains(throwed, "Dodano Klienta");
        }

        [TestMethod]
        public void CorrectMessageOfAddedCustomer()
        {
            //when 
            command.Name = "Maciej";
            command.Surname = "Hanulak";
            command.Address = "ul. Moja";
            command.TelephoneNumber = "123456789";

            //given
            commandBus.Handle(command);
            var customers = queryBus.Handle(new GetCustomers());
            //then
            Assert.IsTrue(customers.Count() == 1);
        }

        [TestMethod]
        public void NullCustomerNotAdded()
        {
            //when
            command.Name = null;
            command.Surname = "Hanulak";
            command.Address = "ul. Moja";
            command.TelephoneNumber = "123456789";

            //given
            var throwed = commandBus.Handle(command);

            //then
            StringAssert.Contains(throwed, "Nie dodano Klienta, ponieważ conajmniej jedno z atrybutów nie zawiera wartości.");
        }

        [TestMethod]
        public void ExistedCustomerNotAdded()
        {
            //when
            command.Name = "Maciej";
            command.Surname = "Hanulak";
            command.Address = "ul. Moja";
            command.TelephoneNumber = "123456789";

            //given
            commandBus.Handle(command);
            var throwed = commandBus.Handle(command);

            //then
            StringAssert.Contains(throwed, "Nie dodano Klienta, ponieważ istnieje on w bazie biblioteki.");
        }
    }
}

