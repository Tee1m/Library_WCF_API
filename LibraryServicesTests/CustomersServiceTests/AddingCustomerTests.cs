using LibraryService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Library.ServicesTests;

namespace CustomersServicesTests
{
    [TestClass]
    public class AddingCustomerTests
    {
        CustomerDTO testCustomer = new CustomerDTOBuilder()
            .SetId(1)
            .SetName("Test")
            .SetSurname("Test")
            .SetAddress("Test")
            .SetTelephoneNumber("123")
            .Build();

        CustomerDTO anotherCustomer = new CustomerDTOBuilder()
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
            CustomerDTO nullCustomer = new CustomerDTO();

            var customersRepository = MockFactory.CreateCustomersRepository(new List<CustomerDTO>() { testCustomer });
            var borrowsRepository = MockFactory.CreateBorrowsRepository(new List<BorrowDTO>());

            var customersService = new CustomersService(customersRepository, borrowsRepository);

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
            var customersRepository = MockFactory.CreateCustomersRepository(new List<CustomerDTO>() { testCustomer });
            var borrowsRepository = MockFactory.CreateBorrowsRepository(new List<BorrowDTO>());

            var customersService = new CustomersService(customersRepository, borrowsRepository);

            //given
            var throwed = customersService.AddCustomer(testCustomer);
            var expected = "Nie dodano Klienta, ponieważ istnieje on w bazie biblioteki.";
           
            //then
            StringAssert.Contains(throwed, expected);
        }

        [TestMethod]
        public void CorrectCustomerAdded()
        {
            //when
            var customersRepository = MockFactory.CreateCustomersRepository(new List<CustomerDTO>() { testCustomer });
            var borrowsRepository = MockFactory.CreateBorrowsRepository(new List<BorrowDTO>());

            var customersService = new CustomersService(customersRepository, borrowsRepository);

            //given
            var throwed = customersService.AddCustomer(anotherCustomer);
            var expected = $"Dodano Klienta, P. {anotherCustomer.Name} {anotherCustomer.Surname}";

            //then
            StringAssert.Contains(throwed, expected);
        }
    }
}
