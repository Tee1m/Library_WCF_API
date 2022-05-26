using Application;
using Autofac;
using Domain;
using LibraryHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Common;

namespace Library.IntegrationTests
{
    [TestClass]
    public class AddBorrowTests : TransactionIsolator
    {
        private static IBorrowsService _borrowsService;
        private static ICustomersService _customersService;
        private static IBooksService _booksService;

        Customer testCustomer = new CustomerBuilder()
            .SetId(1)
            .SetName("Test")
            .SetSurname("Test")
            .SetAddress("Test")
            .SetTelephoneNumber("123")
            .Build();

        Book testBook = new BookBuilder()
            .SetId(1)
            .SetTitle("Test")
            .SetAuthorName("Test")
            .SetAuthorSurname("Test")
            .SetAvailability(0)
            .SetDescription("Test")
            .Build();

        [ClassInitialize]
        public static void SetUpTests(TestContext testContext)
        {
            var container = ContainerIoC.RegisterContainerBuilder().Build();
            _borrowsService = container.BeginLifetimeScope().Resolve<IBorrowsService>();
            _customersService = container.BeginLifetimeScope().Resolve<ICustomersService>();
            _booksService = container.BeginLifetimeScope().Resolve<IBooksService>();
        }

        [TestMethod]
        [DataRow("Wpożyczenie jest niemożliwe, ponieważ książki niema na stanie biblioteki.")]
        public void WhenBookBookIsOutOfStockBorrowNotAdded(string message)
        {
            //when
            _booksService.AddBook(testBook);
            _customersService.AddCustomer(testCustomer);
            var customerId = _customersService.GetCustomers().Select(x => x.Id).First();
            var bookId = _booksService.GetBooks().Select(x => x.Id).First();
            //given 
            var throwed = _borrowsService.AddBorrow(customerId, bookId);
            var expected = message;

            //then
            StringAssert.Contains(throwed, expected);
        }

        [TestMethod]
        [DataRow("Nie znaleziono wskazanego Klienta w bazie biblioteki.")]
        public void WhenCustomerNotFoundBorrowNotAdded(string message)
        {
            //when
            _booksService.AddBook(testBook);

            var bookId = _booksService.GetBooks().Select(x => x.Id).First();
            //given 
            var throwed = _borrowsService.AddBorrow(-1, bookId);
            var expected = message;

            //then
            StringAssert.Contains(throwed, expected);
        }

        [TestMethod]
        [DataRow("Nie znaleziono wskazanej Książki w bazie biblioteki.")]
        public void WhenBookNotFoundBorrowNotAdded(string message)
        {
            //when
            _customersService.AddCustomer(testCustomer);

            var customerId = _customersService.GetCustomers().Select(x => x.Id).First();
            //given 
            var throwed = _borrowsService.AddBorrow(customerId, -1);
            var expected = message;

            //then
            StringAssert.Contains(throwed, expected);
        }

        [TestMethod]
        public void BorrowAdded()
        {
            //when
            testBook.Amount++;

            _booksService.AddBook(testBook);
            _customersService.AddCustomer(testCustomer);

            var customerId = _customersService.GetCustomers().Select(x => x.Id).First();
            var bookId = _booksService.GetBooks().Select(x => x.Id).First();
            //given

            var throwed = _borrowsService.AddBorrow(customerId, bookId);
            var expected = $"Wyporzyczono, Tytuł: {testBook.Title}, Klientowi: {testCustomer.Name} {testCustomer.Surname}";

            //then
            StringAssert.Contains(throwed, expected);
        }
    }
}
