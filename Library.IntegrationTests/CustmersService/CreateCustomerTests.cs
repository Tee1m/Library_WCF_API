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

namespace CustomerServicesTests
{
    [TestClass]
    public class CreateCustomerTests : TransactionIsolator
    {
        //private static ServiceHost host;
        ////EndpointAddress baseAddress = new EndpointAddress("http://localhost:8732/TestAddress/Service");
        //EndpointAddress ep = new EndpointAddress("http://localhost:8000/testService");
        ////Uri baseAddress = new Uri("http://localhost:8000");
        //Binding binding = new BasicHttpBinding();

        //[ClassInitialize()]
        //public static void TestHostInitialize(TestContext testContext)
        //{

        //    host = new ServiceHost(typeof(CustomersService), new Uri("http://localhost:8000"));
        //    //host = new ServiceHost(typeof(CustomersService));

        //    host.AddServiceEndpoint(typeof(ICommandBus), new WebHttpBinding(), "testService", new Uri("http://localhost:8000"));

        //    var container = ContainerIoC.RegisterContainerBuilder().Build();

        //    host.AddDependencyInjectionBehavior<ICommandBus>(container);

        //    host.Open();
        //}

        //[ClassCleanup()]
        //public static void TestHostDispose()
        //{
        //    host.Close();
        //}

        //[TestMethod]
        //public void NewCustomerAdded()
        //{
        //    //when 
        //    CustomerDTO customer = new CustomerDTO();
        //    customer.Name = "Maciej1";
        //    customer.Surname = "Hanulak";
        //    customer.Address = "ul. Moja";
        //    customer.TelephoneNumber = "123456789";

        //    WebChannelFactory<ICommandBus> proxy = new WebChannelFactory<ICommandBus>(new WebHttpBinding(), ep.Uri);
        //    ICommandBus service = proxy.CreateChannel();

        //    //var container = ContainerIoC.RegisterContainerBuilder().Build();

        //    //var service = container.BeginLifetimeScope().Resolve<ICommandBus>();

        //    //ICommandBus service = (CustomersService)factory.CreateChannel(baseAddress);
        //    //given
        //    var resultString = service.AddCustomer(customer);
        //    var result = service.GetCustomers();
        //    //then
        //    Assert.IsNotNull(resultString);

        //}

        private ICommandBus commandBus = new CommandBus();
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
            var throwed = commandBus.Handle<CreateCustomer>(command);

            //then
            StringAssert.Contains(throwed, "Dodano Klienta");
        }

        //[TestMethod]
        //public void CorrectMessageOfAddedCustomer()
        //{
        //    //when
        //    customer.Name = "Maciej";
        //    customer.Surname = "Hanulak";
        //    customer.Address = "ul. Moja";
        //    customer.TelephoneNumber = "123456789";

        //    //given
        //    var message = service.AddCustomer(customer);

        //    //then
        //    StringAssert.Contains(message, $"Dodano Klienta, P. {customer.Name} {customer.Surname}");
        //}

        [TestMethod]
        public void NullCustomerNotAdded()
        {
            //when
            command.Name = null;
            command.Surname = "Hanulak";
            command.Address = "ul. Moja";
            command.TelephoneNumber = "123456789";

            //given
            var throwed = commandBus.Handle<CreateCustomer>(command);

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
            commandBus.Handle<CreateCustomer>(command);
            var throwed = commandBus.Handle<CreateCustomer>(command);

            //then
            StringAssert.Contains(throwed, "Nie dodano Klienta, ponieważ istnieje on w bazie biblioteki.");
        }
    }
}

