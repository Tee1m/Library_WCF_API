using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryHost;
using LibraryService;
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

namespace CustomerServicesTests
{
    [TestClass]
    public class AddingCustomerTests : TransactionIsolator
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

        //    host.AddServiceEndpoint(typeof(ICustomersService), new WebHttpBinding(), "testService", new Uri("http://localhost:8000"));

        //    var container = ContainerIoC.RegisterContainerBuilder().Build();

        //    host.AddDependencyInjectionBehavior<ICustomersService>(container);

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

        //    WebChannelFactory<ICustomersService> proxy = new WebChannelFactory<ICustomersService>(new WebHttpBinding(), ep.Uri);
        //    ICustomersService service = proxy.CreateChannel();

        //    //var container = ContainerIoC.RegisterContainerBuilder().Build();

        //    //var service = container.BeginLifetimeScope().Resolve<ICustomersService>();

        //    //ICustomersService service = (CustomersService)factory.CreateChannel(baseAddress);
        //    //given
        //    var resultString = service.AddCustomer(customer);
        //    var result = service.GetCustomers();
        //    //then
        //    Assert.IsNotNull(resultString);

        //}

        private static ICustomersService service;

        private CustomerDTO customer = new CustomerDTO();

        [ClassInitialize]
        public static void SetUpTests(TestContext context)
        {
            var container = ContainerIoC.RegisterContainerBuilder().Build();
            service = container.BeginLifetimeScope().Resolve<ICustomersService>();
        }

        [TestMethod]
        public void AddNewCustomer()
        {
            //when 
            customer.Name = "Maciej";
            customer.Surname = "Hanulak";
            customer.Address = "ul. Moja";
            customer.TelephoneNumber = "123456789";

            service.AddCustomer(customer);

            //given
            var customers = service.GetCustomers();
    
            //then
            Assert.IsTrue(customers.Count == 1);
        }

        [TestMethod]
        public void CorrectMessageOfAddedCustomer()
        {
            //when
            customer.Name = "Maciej";
            customer.Surname = "Hanulak";
            customer.Address = "ul. Moja";
            customer.TelephoneNumber = "123456789";

            //given
            var message = service.AddCustomer(customer);

            //then
            StringAssert.Contains(message, $"Dodano Klienta, P. {customer.Name} {customer.Surname}");
        }

        [TestMethod]
        public void NullCustomerNotAdded()
        {
            //when
            customer.Name = null;
            customer.Surname = "Hanulak";
            customer.Address = "ul. Moja";
            customer.TelephoneNumber = "123456789";

            //given
            var message = service.AddCustomer(customer);

            //then
            StringAssert.Contains(message, "Nie dodano Klienta, ponieważ conajmniej jedno z atrybutów nie zawiera wartości.");
        }

        [TestMethod]
        public void ExistedCustomerNotAdded()
        {
            //when
            customer.Name = "Maciej";
            customer.Surname = "Hanulak";
            customer.Address = "ul. Moja";
            customer.TelephoneNumber = "123456789";

            service.AddCustomer(customer);
            //given

            var message = service.AddCustomer(customer);

            //then
            StringAssert.Contains(message, "Nie dodano Klienta, ponieważ istnieje on w bazie biblioteki.");
        }
    }
}

