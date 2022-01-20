using LibraryService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace CustomersServicesTests
{
    [TestClass]
    public class AddingCustomerTests
    {
        Customer testCustomer = new CustomerBuilder()
            .SetId(1)
            .SetName("Test")
            .SetSurname("Test")
            .SetAddress("Test")
            .SetTelephoneNumber("123")
            .Build();

        Customer anotherCustomer = new CustomerBuilder()
            .SetId(2)
            .SetName("TestTest")
            .SetSurname("TestTest")
            .SetAddress("TestTest")
            .SetTelephoneNumber("1232")
            .Build();

        [TestMethod]
        public void NullCustomerNotAdded()
        {
            //when
            Customer nullCustomer = new Customer();

            var dBClient = MockDataBaseClient(new List<Customer>());
            var customersService = new CustomersService(dBClient);

            //given
            var throwed = customersService.AddCustomer(nullCustomer);
            var expected = "Nie dodano Klienta, ponieważ conajmniej jedno z atrybutów nie zawiera wartości.";

            //then
            StringAssert.Contains(throwed, expected);
        }

        [TestMethod]
        public void ExistCustomerNotAdded()
        {
            //when
            var dBClient = MockDataBaseClient(new List<Customer>() { testCustomer });
            var customersService = new CustomersService(dBClient);

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
            var dBClient = MockDataBaseClient(new List<Customer>() { anotherCustomer });
            var customersService = new CustomersService(dBClient);

            //given
            var throwed = customersService.AddCustomer(testCustomer);
            var expected = $"Dodano Klienta, P. {testCustomer.Name} {testCustomer.Surname}";

            //then
            StringAssert.Contains(throwed, expected);
        }
        
        IDatabaseClient MockDataBaseClient(List<Customer> customersList)
        {
            var mockDBClient = new Mock<IDatabaseClient>();

            mockDBClient.Setup(x => x.GetCustomers())
                .Returns(customersList);

            return mockDBClient.Object;
        }
    }
}
