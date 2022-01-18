using LibraryService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace CustomerServicesTests
{
    [TestClass]
    public class AddingCustomerTests
    {
        Customer testCustomer = new Customer("Test", "Test", "Test", "Test");
        Customer anotherCustomer = new Customer("Another", "Another", "Another", "Another");

        [TestMethod]
        public void NullCustomerNotAdded()
        {
            //when
            testCustomer = new Customer();
            var customersService = MockCustomerService(new List<Customer>() { testCustomer });

            //given
            var throwed = customersService.AddCustomer(testCustomer);
            var expected = "Nie dodano Klienta, ponieważ conajmniej jedno z atrybutów nie zawiera wartości.";

            //then

            StringAssert.Contains(throwed, expected);

        }

        [TestMethod]
        public void ExistCustomerNotAdded()
        {
            //when
            var customersService = MockCustomerService(new List<Customer>() { testCustomer });
     
            //given
            var throwed = customersService.AddCustomer(testCustomer);
            var expected = "Nie dodano Klienta, ponieważ istnieje on w bazie biblioteki.";
           
            //then
            StringAssert.Contains(throwed, expected);
        }

        [TestMethod]
        public void CorectCustomerAdded()
        {
            //when
            var customersService = MockCustomerService(new List<Customer>() {anotherCustomer});

            //given
            var throwed = customersService.AddCustomer(testCustomer);
            var expected = $"Dodano Klienta, P. {testCustomer.Name} {testCustomer.Surname}";

            //then
            StringAssert.Contains(throwed, expected);
        }
        
        public CustomersService MockCustomerService(List<Customer> customersList)
        {
            var mockCustomerService = new Mock<IDatabaseClient>();

            mockCustomerService.Setup(x => x.GetCustomers())
                .Returns(customersList);

            return new CustomersService(mockCustomerService.Object);
        }
    }
}
