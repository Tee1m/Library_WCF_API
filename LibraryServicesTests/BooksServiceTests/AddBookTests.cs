using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Library.ServicesTests;

namespace BooksServiceTests
{
    [TestClass]
    public class AddBookTests
    {
        BookDTO book = new BookDTOBuilder()
            .SetId(1)
            .SetTitle("Test")
            .SetAuthorName("Test")
            .SetAuthorSurname("Test")
            .SetAvailability(1)
            .Build();

        BookDTO anotherBook = new BookDTOBuilder()
            .SetId(2)
            .SetTitle("TestTest")
            .SetAuthorName("TestTest")
            .SetAuthorSurname("TestTest")
            .SetAvailability(2)
            .Build();

        [TestMethod]
        public void NullBookNotAdded()
        {
            //when
            var booksRepository = MockFactory.CreateBooksRepository(new List<BookDTO>() { book });
            var borrowsRepository = MockFactory.CreateBorrowsRepository(new List<BorrowDTO>());

            var booksService = new BooksService(booksRepository, borrowsRepository);

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
            var booksRepository = MockFactory.CreateBooksRepository(new List<BookDTO>() { book });
            var borrowsRepository = MockFactory.CreateBorrowsRepository(new List<BorrowDTO>());

            var booksService = new BooksService(booksRepository, borrowsRepository);

            //given
            var throwed = booksService.AddBook(book);
            var expected = "Książka znajduje się w bazie biblioteki. Uzupełniono jej dostępność.";

            //then
            StringAssert.Contains(throwed, expected);
        }

        [TestMethod]
        public void BookAdded()
        {
            //when
            var booksRepository = MockFactory.CreateBooksRepository(new List<BookDTO>() { book });
            var borrowsRepository = MockFactory.CreateBorrowsRepository(new List<BorrowDTO>());

            var booksService = new BooksService(booksRepository, borrowsRepository);

            //given
            var throwed = booksService.AddBook(anotherBook);
            var expected = "Dodano Książkę do bazy danych.";

            //then
            StringAssert.Contains(throwed, expected);
        }
    }
}
