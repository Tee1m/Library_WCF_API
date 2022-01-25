using LibraryService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Library.ServicesTests;

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
            var customersRepository = MockFactory.CreateCustomersRepository(new List<Customer>() { testCustomer });
            var borrowsRepository = MockFactory.CreateBorrowsRepository(new List<Borrow>() { testBorrow });

            var customersService = new CustomersService(customersRepository, borrowsRepository);

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
            var customersRepository = MockFactory.CreateCustomersRepository(new List<Customer>() { testCustomer });
            var borrowsRepository = MockFactory.CreateBorrowsRepository(new List<Borrow>() { testBorrow });

            var customersService = new CustomersService(customersRepository, borrowsRepository);

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

            var customersRepository = MockFactory.CreateCustomersRepository(new List<Customer>() { testCustomer });
            var borrowsRepository = MockFactory.CreateBorrowsRepository(new List<Borrow>() { anotherBorrow });

            var customersService = new CustomersService(customersRepository, borrowsRepository);

            //given
            var throwed = customersService.DeleteCustomer(1);
            var expected = $"Usunięto Klienta, P. {testCustomer.Name} {testCustomer.Surname}.";

            //then
            StringAssert.Contains(throwed, expected);
        }
    }
}
