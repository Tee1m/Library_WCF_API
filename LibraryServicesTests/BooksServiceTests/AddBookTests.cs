using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BooksServiceTests
{
    [TestClass]
    public class AddBookTests
    {
        Book book = new BookBuilder()
            .SetId(1)
            .SetTitle("Test")
            .SetAuthorName("Test")
            .SetAuthorSurname("Test")
            .SetAvailability(1)
            .SetDescription("Test")
            .Build();

        Book anotherBook = new BookBuilder()
            .SetId(2)
            .SetTitle("TestTest")
            .SetAuthorName("TestTest")
            .SetAuthorSurname("TestTest")
            .SetAvailability(2)
            .SetDescription("....")
            .Build();

        [TestMethod]
        public void NullBookNotAdded()
        {
            //when
            var dBClient = MockDataBaseClient(new List<Book>() { book });
            var booksService = new BooksService(dBClient);

            book.Title = null;
            //given
            var throwed = booksService.AddBook(book);
            var expected = "Nie dodano książki, ponieważ conajmniej jedno z atrybutów nie zawiera wartości.";

            //then
            StringAssert.Contains(throwed, expected);
        }

        [TestMethod]
        public void ExistingBookNotAdded()
        {
            //when
            var dBClient = MockDataBaseClient(new List<Book>() { book });
            var booksService = new BooksService(dBClient);

            //given
            var throwed = booksService.AddBook(book);
            var expected = "Książka znajduje się w bazie biblioteki. Uzupełniono jej dostępność.";

            //then
            StringAssert.Contains(throwed, expected);
        }

        [TestMethod]
        public void AddedCorrectBook()
        {
            //when
            var dBClient = MockDataBaseClient(new List<Book>() { book });
            var booksService = new BooksService(dBClient);

            //given
            var throwed = booksService.AddBook(anotherBook);
            var expected = "Dodano Książkę do bazy danych.";

            //then
            StringAssert.Contains(throwed, expected);
        }

        IDatabaseClient MockDataBaseClient(List<Book> booksList)
        {
            var mockDBClient = new Mock<IDatabaseClient>();

            mockDBClient.Setup(x => x.GetBooks())
                .Returns(booksList);

            return mockDBClient.Object;
        }
    }
}
