using LibraryService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace CustomerServicesTests
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

            var customersService = MockCustomerService(new List<Customer>() { testCustomer }, new List<Borrow>());

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
            
            var customersService = MockCustomerService(new List<Customer>() { testCustomer },
                new List<Borrow>() { testBorrow });

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

            var customerService = MockCustomerService(new List<Customer>() { testCustomer },
                new List<Borrow>() { testBorrow });

            //given
            var throwed = customerService.DeleteCustomer(1);
            var expected = $"Usunięto Klienta, P. {testCustomer.Name} {testCustomer.Surname}.";

            //then
            StringAssert.Contains(throwed, expected);
        }

        public CustomersService MockCustomerService(List<Customer> customersList, List<Borrow> borrowsList)
        {
            var mockCustomerService = new Mock<IDatabaseClient>();

            mockCustomerService.Setup(x => x.GetCustomers())
                .Returns(customersList);

            mockCustomerService.Setup(y => y.GetBorrows())
                .Returns(borrowsList);

            return new CustomersService(mockCustomerService.Object);
        }
    }
}
