using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.IntegrationTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryHost;
using LibraryService;
using Autofac;

namespace BooksServiceTests
{ 
    [TestClass]
    public class AddingBookTests : TransactionIsolator
    {
        private static IBooksService _booksService;

        BookDTO book = new BookDTO();

        [ClassInitialize]
        public static void SetUpTests(TestContext testContext)
        {
            var container = ContainerIoC.RegisterContainerBuilder().Build();
            _booksService = container.BeginLifetimeScope().Resolve<IBooksService>();
        }

        [TestMethod]
        public void AddNewBook()
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

            //then
            Assert.IsTrue(books.Count == 1);
        }

        [TestMethod]
        public void CorrectMessageOfAddedBookOperation()
        {
            //when
            book.AuthorName = "Test";
            book.AuthorSurname = "testName";
            book.Amount = 4;
            book.Description = "test test test";
            book.Title = "TestTitle";

            //given
            var message = _booksService.AddBook(book);

            //then
            StringAssert.Contains(message, "Dodano Książkę do bazy danych.");
        }

        [TestMethod]
        public void NullBookNotAdded()
        {
            //when
            book.AuthorName = null;
            book.AuthorSurname = "testName";
            book.Amount = 3;
            book.Description = "test test test";
            book.Title = "TestTitle";

            //given
            var message = _booksService.AddBook(book);

            //then
            StringAssert.Contains(message, "Nie dodano książki, ponieważ conajmniej jedno z atrybutów nie zawiera wartości.");
        }

        [TestMethod]
        public void ExistedBookReplenishedQuantity()
        {
            //when
            book.AuthorName = "Test";
            book.AuthorSurname = "testName";
            book.Amount = 3;
            book.Description = "test test test";
            book.Title = "TestTitle";

            //given
            _booksService.AddBook(book);
            var message = _booksService.AddBook(book);

            //then
            StringAssert.Contains(message, "Książka znajduje się w bazie biblioteki. Uzupełniono jej dostępność.");
        }
    }
}
