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
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(x => x.BooksRepository.Get()).Returns(new List<BookDTO>() { book });
            unitOfWork.Setup(x => x.BorrowsRepository.Get()).Returns(new List<BorrowDTO>());

            var booksService = new BooksService(unitOfWork.Object);

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
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(x => x.BooksRepository.Get()).Returns(new List<BookDTO>() { book });
            unitOfWork.Setup(x => x.BorrowsRepository.Get()).Returns(new List<BorrowDTO>());

            var booksService = new BooksService(unitOfWork.Object);

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
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(x => x.BooksRepository.Get()).Returns(new List<BookDTO>() { book });
            unitOfWork.Setup(x => x.BorrowsRepository.Get()).Returns(new List<BorrowDTO>());

            var booksService = new BooksService(unitOfWork.Object);

            //given
            var throwed = booksService.AddBook(anotherBook);
            var expected = "Dodano Książkę do bazy danych.";

            //then
            StringAssert.Contains(throwed, expected);
        }
    }
}
