using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryService;
using Library.ServicesTests;

namespace BooksServiceTests
{
    [TestClass]
    public class DeleteBookTests
    {
        Book book = new BookBuilder()
            .SetId(1)
            .SetTitle("Test")
            .SetAuthorName("Test")
            .SetAuthorSurname("Test")
            .SetAvailability(1)
            .SetDescription("Test")
            .Build();

        Borrow borrow = new BorrowBuilder()
            .SetId(1)
            .SetBookId(1)
            .SetCustomerId(1)
            .SetDateOfBorrow(DateTime.Now)
            .Build();
        
        [TestMethod]
        public void BookNotExistAndNotDeleted()
        {
            //when
            var booksRepository = MockFactory.CreateBooksRepository(new List<Book>() { book });
            var borrowsRepository = MockFactory.CreateBorrowsRepository(new List<Borrow>() { borrow });

            var booksService = new BooksService(booksRepository, borrowsRepository);

            //given
            var throwed = booksService.DeleteBook(2);
            var expected = "Nie znaleziono wskazanej Książki w bazie biblioteki.";

            //then
            StringAssert.Contains(throwed, expected);
        }

        [TestMethod]
        public void NotReturnedAllCopiesBookNotDeleted()
        {
            //when
            var booksRepository = MockFactory.CreateBooksRepository(new List<Book>() { book });
            var borrowsRepository = MockFactory.CreateBorrowsRepository(new List<Borrow>() { borrow });

            var booksService = new BooksService(booksRepository, borrowsRepository);

            //given
            var throwed = booksService.DeleteBook(1);
            var expected = "Nie usunieto książki, ponieważ nie wszystkie egzemplarze zostały zwrócone";

            //then
            StringAssert.Contains(throwed, expected);
        }

        [TestMethod]
        public void CorrectBookAreDeleted()
        {
            //when
            borrow.BookId = 2;
            var booksRepository = MockFactory.CreateBooksRepository(new List<Book>() { book });
            var borrowsRepository = MockFactory.CreateBorrowsRepository(new List<Borrow>() { borrow });

            var booksService = new BooksService(booksRepository, borrowsRepository);

            //given
            var throwed = booksService.DeleteBook(1);
            var expected = $"Usunięto książkę, Tytuł: {book.Title} Autor: {book.AuthorName} {book.AuthorSurname}.";

            //then
            StringAssert.Contains(throwed, expected);
        }
    }
}
