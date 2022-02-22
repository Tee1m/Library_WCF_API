﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryService;
using Moq;

namespace BooksServiceTests
{
    [TestClass]
    public class DeleteBookTests
    {
        BookDTO book = new BookDTOBuilder()
            .SetId(1)
            .SetTitle("Test")
            .SetAuthorName("Test")
            .SetAuthorSurname("Test")
            .SetAvailability(1)
            .SetDescription("Test")
            .Build();

        BorrowDTO borrow = new BorrowDTOBuilder()
            .SetId(1)
            .SetBookId(1)
            .SetCustomerId(1)
            .SetDateOfBorrow(DateTime.Now)
            .Build();
        
        [TestMethod]
        public void BookNotExistAndNotDeleted()
        {
            //when
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(a => a.BooksRepository.Get()).Returns(new List<BookDTO>() { book });
            unitOfWork.Setup(a => a.BorrowsRepository.Get()).Returns(new List<BorrowDTO>() { borrow });

            var booksService = new BooksService(unitOfWork.Object);

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
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(a => a.BooksRepository.Get()).Returns(new List<BookDTO>() { book });
            unitOfWork.Setup(a => a.BorrowsRepository.Get()).Returns(new List<BorrowDTO>() { borrow });

            var booksService = new BooksService(unitOfWork.Object);

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
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(a => a.BooksRepository.Get()).Returns(new List<BookDTO>() { book });
            unitOfWork.Setup(a => a.BorrowsRepository.Get()).Returns(new List<BorrowDTO>() { borrow });

            var booksService = new BooksService(unitOfWork.Object);

            //given
            var throwed = booksService.DeleteBook(1);
            var expected = $"Usunięto książkę, Tytuł: {book.Title} Autor: {book.AuthorName} {book.AuthorSurname}.";

            //then
            StringAssert.Contains(throwed, expected);
        }
    }
}
