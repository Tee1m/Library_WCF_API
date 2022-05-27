using Application;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Moq;
using Domain;
using Tests.Common;

namespace ApplicationTests
{
    [TestClass]
    public class AddCustomerTests
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

            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(a => a.CustomersRepository.Get()).Returns(new List<Customer>() { testCustomer });
            unitOfWork.Setup(a => a.BorrowsRepository.Get()).Returns(new List<Borrow>());

            var customersService = new CustomersService(unitOfWork.Object, new CustomerUniquenessChecker(unitOfWork.Object.CustomersRepository));

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
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(a => a.CustomersRepository.Get()).Returns(new List<Customer>() { testCustomer });
            unitOfWork.Setup(a => a.BorrowsRepository.Get()).Returns(new List<Borrow>());

            var customersService = new CustomersService(unitOfWork.Object, new CustomerUniquenessChecker(unitOfWork.Object.CustomersRepository));

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
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(a => a.CustomersRepository.Get()).Returns(new List<Customer>() { testCustomer });
            unitOfWork.Setup(a => a.BorrowsRepository.Get()).Returns(new List<Borrow>());

            var customersService = new CustomersService(unitOfWork.Object, new CustomerUniquenessChecker(unitOfWork.Object.CustomersRepository));

            //given
            var throwed = customersService.AddCustomer(anotherCustomer);
            var expected = $"Dodano Klienta, P. {anotherCustomer.Name} {anotherCustomer.Surname}";

            //then
            StringAssert.Contains(throwed, expected);
        }
    }
}
