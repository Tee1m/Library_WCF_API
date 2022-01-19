using LibraryService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace CustomersServicesTests
{
    [TestClass]
    public class DeleteCustomerTests
    {
        Customer testCustomer = new Customer("Test", "Test", "Test", "Test");
        Borrow testBorrow = new Borrow();
        [TestMethod]
        public void NonExistingCustomerNotDeleted()
        {
            //when
            testCustomer.Id = 1;

            var dBClient = MockDataBaseClient(new List<Customer>() { testCustomer }, new List<Borrow>());
            var customersService = new CustomersService(dBClient);

            //given
            var throwed = customersService.DeleteCustomer(2);
            var expected = "Nie znaleziono wskazanego Klienta w bazie biblioteki.";

            //then
            StringAssert.Contains(throwed, expected);
        }

        [TestMethod]
        public void CustomerWithBorrowNotDeleted()
        {
            //when
            testCustomer.Id = 1;
            testBorrow.CustomerId = 1;
            
            var dBClient = MockDataBaseClient(new List<Customer>() { testCustomer }, new List<Borrow>() { testBorrow });
            var customersService = new CustomersService(dBClient);

            //given
            var throwed = customersService.DeleteCustomer(1);
            var expected = "Nie usunieto Klienta, ponieważ posiada on jeszcze wypożyczone książki.";

            //then
            StringAssert.Contains(throwed, expected);
        }

        [TestMethod]
        public void CustomerWithoutBorrowDeleted()
        {
            //when
            testCustomer.Id = 1;
            testBorrow.CustomerId = 2;

            var dBClient = MockDataBaseClient(new List<Customer>() { testCustomer }, new List<Borrow>() { testBorrow });
            var customerService = new CustomersService(dBClient);

            //given
            var throwed = customerService.DeleteCustomer(1);
            var expected = $"Usunięto Klienta, P. {testCustomer.Name} {testCustomer.Surname}.";

            //then
            StringAssert.Contains(throwed, expected);
        }

        IDatabaseClient MockDataBaseClient(List<Customer> customersList, List<Borrow> borrowsList)
        {
            var mockDBClient = new Mock<IDatabaseClient>();

            mockDBClient.Setup(x => x.GetCustomers())
                .Returns(customersList);

            mockDBClient.Setup(y => y.GetBorrows())
                .Returns(borrowsList);

            return mockDBClient.Object;
        }
    }
}
