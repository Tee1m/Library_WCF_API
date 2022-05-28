//using Application;
//using Autofac;
//using Domain;
//using LibraryHost;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Tests.Common;

//namespace Library.IntegrationTests
//{
//    [TestClass]
//    public class CreateBorrowTests : TransactionIsolator
//    {
//        private static ICommandBus _commandBus = new CommandBus();

//        [TestMethod]
//        [DataRow("Wpożyczenie jest niemożliwe, ponieważ książki niema na stanie biblioteki.")]
//        public void WhenBookBookIsOutOfStockBorrowNotAdded(string message)
//        {
//            //when
//            //_booksService.AddBook(testBook);
//            CreateBook createBookCommand = new CreateBook();
//            createBookCommand.AuthorName = "Test";
//            createBookCommand.AuthorSurname = "testName";
//            createBookCommand.Amount = 4;
//            createBookCommand.Title = "TestTitle";
//            createBookCommand.Description = "TestDesc";

//            CreateCustomer createCustomerCommand = new CreateCustomer();

//            createCustomerCommand.Name = "Maciej";
//            createCustomerCommand.Surname = "Hanulak";
//            createCustomerCommand.Address = "ul. Moja";
//            createCustomerCommand.TelephoneNumber = "123456789";

//            //given
//            _commandBus.Handle<CreateBook>(createBookCommand);
//            _commandBus.Handle<CreateCustomer>(createCustomerCommand);

//            _customersService.AddCustomer(testCustomer);
//            var CustomerId = _customersService.GetCustomers().Select(x => x.Id).First();
//            var BookId = _booksService.GetBooks().Select(x => x.Id).First();
//            //given 
//            var throwed = _borrowsService.AddBorrow(CustomerId, BookId);
//            var expected = message;

//            //then
//            StringAssert.Contains(throwed, expected);
//        }

//        [TestMethod]
//        [DataRow("Nie znaleziono wskazanego Klienta w bazie biblioteki.")]
//        public void WhenCustomerNotFoundBorrowNotAdded(string message)
//        {
//            //when
//            _booksService.AddBook(testBook);

//            var BookId = _booksService.GetBooks().Select(x => x.Id).First();
//            //given 
//            var throwed = _borrowsService.AddBorrow(-1, BookId);
//            var expected = message;

//            //then
//            StringAssert.Contains(throwed, expected);
//        }

//        [TestMethod]
//        [DataRow("Nie znaleziono wskazanej Książki w bazie biblioteki.")]
//        public void WhenBookNotFoundBorrowNotAdded(string message)
//        {
//            //when
//            _customersService.AddCustomer(testCustomer);

//            var CustomerId = _customersService.GetCustomers().Select(x => x.Id).First();
//            //given 
//            var throwed = _borrowsService.AddBorrow(CustomerId, -1);
//            var expected = message;

//            //then
//            StringAssert.Contains(throwed, expected);
//        }

//        [TestMethod]
//        public void BorrowAdded()
//        {
//            //when
//            testBook.Amount++;

//            _booksService.AddBook(testBook);
//            _customersService.AddCustomer(testCustomer);

//            var CustomerId = _customersService.GetCustomers().Select(x => x.Id).First();
//            var BookId = _booksService.GetBooks().Select(x => x.Id).First();
//            //given

//            var throwed = _borrowsService.AddBorrow(CustomerId, BookId);
//            var expected = $"Wyporzyczono, Tytuł: {testBook.Title}, Klientowi: {testCustomer.Name} {testCustomer.Surname}";

//            //then
//            StringAssert.Contains(throwed, expected);
//        }
//    }
//}
