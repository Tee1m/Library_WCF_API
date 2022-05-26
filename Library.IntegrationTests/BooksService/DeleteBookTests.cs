using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.IntegrationTests;
using LibraryHost;
using Application;
using Autofac;
using Domain;

namespace BooksServiceTests
{
    [TestClass]
    public class DeleteBookTests : TransactionIsolator
    {
        private static IBooksService _booksService;
        private static IBorrowsService _borrowsService;
        private static ICustomersService _customersService;
        private Book book = new Book();

        [ClassInitialize]
        public static void SetUpTests(TestContext testContext)
        {
            var container = ContainerIoC.RegisterContainerBuilder().Build();
            _booksService = container.BeginLifetimeScope().Resolve<IBooksService>();
            _borrowsService = container.BeginLifetimeScope().Resolve<IBorrowsService>();
            _customersService = container.BeginLifetimeScope().Resolve<ICustomersService>();
        }

        [TestMethod]
        public void DeleteBook()
        {
            //when
            book.AuthorName = "Test";
            book.AuthorSurname = "testName";
            book.Amount = 3;
            book.Description = "test test test";
            book.Title = "TestTitle";

            //given
            _booksService.AddBook(book);
            var books = _booksService.GetBooks();

            var bookId = books.Where(a => a.AuthorName == book.AuthorName && a.AuthorSurname == book.AuthorSurname && a.Title == book.Title).Select(a => a.Id).Single();
            _booksService.DeleteBook(bookId);
            books = _booksService.GetBooks();

            //then
            Assert.IsTrue(books.Count == 0);
        }

        [TestMethod]
        public void CorrectMessageOfDeletingBookOperation()
        {
            //when
            book.AuthorName = "Test";
            book.AuthorSurname = "testName";
            book.Amount = 3;
            book.Description = "test test test";
            book.Title = "TestTitle";

            //given
            _booksService.AddBook(book);
            var books = _booksService.GetBooks();

            var bookId = books.Where(a => a.AuthorName == book.AuthorName && a.AuthorSurname == book.AuthorSurname && a.Title == book.Title).Select(a => a.Id).Single();
            var message = _booksService.DeleteBook(bookId);

            //then
            StringAssert.Contains(message, $"Usunięto książkę, Tytuł: {book.Title} Autor: {book.AuthorName} {book.AuthorSurname}.");
        }

        [TestMethod]
        public void NotExistedBookDidNotDeleted()
        {
            //when
            var bookId = 1;

            //given
            var message = _booksService.DeleteBook(bookId);

            //then
            StringAssert.Contains(message, "Nie znaleziono wskazanej Książki w bazie biblioteki.");
        }

        [TestMethod]
        public void DidNotReturnedBookCanNotDeleted()
        {
            //when
            book.AuthorName = "Test";
            book.AuthorSurname = "testName";
            book.Amount = 3;
            book.Description = "test test test";
            book.Title = "TestTitle";

            Customer customer = new Customer();

            customer.Name = "Maciej";
            customer.Surname = "Hanulak";
            customer.Address = "ul. Moja";
            customer.TelephoneNumber = "123456789";

            _booksService.AddBook(book);
            _customersService.AddCustomer(customer);

            var books = _booksService.GetBooks();
            var customers = _customersService.GetCustomers();

            var bookId = books.Where(a => a.AuthorName == book.AuthorName && a.AuthorSurname == book.AuthorSurname && a.Title == book.Title).Select(a => a.Id).Single();
            var customerId = customers.Where(a => a.Name == customer.Name && a.Surname == customer.Surname && a.Address == customer.Address && a.TelephoneNumber == customer.TelephoneNumber).Select(a => a.Id).Single();
            
            //given
            _borrowsService.AddBorrow(customerId, bookId);
            var message = _booksService.DeleteBook(bookId);

            //then
            StringAssert.Contains(message, "Nie usunieto książki, ponieważ nie wszystkie egzemplarze zostały zwrócone");
        }
    }
}
