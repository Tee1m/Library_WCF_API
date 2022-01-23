using LibraryService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace CustomersServicesTests
{
    [TestClass]
    public class DeleteCustomerTests
    {
        Customer testCustomer = new CustomerBuilder()
            .SetId(1)
            .SetName("Test")
            .SetSurname("Test")
            .SetAddress("Test")
            .SetTelephoneNumber("Test")
            .Build();

        Borrow testBorrow = new BorrowBuilder()
            .SetCustomerId(1)
            .Build();

        [TestMethod]
        public void NonExistingCustomerNotDeleted()
        {
            //when
            var dBClient = MockDataBaseClient(new List<Customer>() { testCustomer }, new List<Borrow>() { testBorrow });
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
            Borrow anotherBorrow = new BorrowBuilder()
            .SetCustomerId(2)
            .Build();

            var dBClient = MockDataBaseClient(new List<Customer>() { testCustomer }, new List<Borrow>() { anotherBorrow });
            var customerService = new CustomersService(dBClient);

            //given
            var throwed = customerService.DeleteCustomer(1);
            var expected = $"Usunięto Klienta, P. {testCustomer.Name} {testCustomer.Surname}.";

            //then
            StringAssert.Contains(throwed, expected);
        }

        IDataBaseClient MockDataBaseClient(List<Customer> customersList, List<Borrow> borrowsList)
        {
            var mockDBClient = new Mock<IDataBaseClient>();

            mockDBClient.Setup(x => x.GetCustomers())
                .Returns(customersList);

            mockDBClient.Setup(y => y.GetBorrows())
                .Returns(borrowsList);

            return mockDBClient.Object;
        }
    }
}
