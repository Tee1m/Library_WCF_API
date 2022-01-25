using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryService;
using Library.ServicesTests;

namespace BorrowServiceTests
{
    [TestClass]
    public class ReturnBorrowTests
    {
        Customer testCustomer = new CustomerBuilder()
            .SetId(1)
            .Build();

        Book testBook = new BookBuilder()
            .SetId(1)
            .SetAvailability(0)
            .Build();

        Borrow testBorrow = new BorrowBuilder()
            .SetId(1)
            .SetCustomerId(1)
            .SetBookId(1)
            .SetReturnDate(DateTime.Now)
            .Build();

        [TestMethod]
        [DataRow(2, "Nie znaleziono wskazanego wypożyczenia w bazie biblioteki.")]
        [DataRow(1, "Wypożyczenie zostało już zwrócone.")]
        public void ExceptionBorrowNotReturned(int borrowId, string announcement)
        {
            //when
            var customerRepository = MockFactory.CreateCustomersRepository(new List<Customer>() { testCustomer });
            var borrowsRepository = MockFactory.CreateBorrowsRepository(new List<Borrow>() { testBorrow });
            var booksRepository = MockFactory.CreateBooksRepository(new List<Book>() { testBook });

            var borrowsService = new BorrowsService(customerRepository, borrowsRepository, booksRepository);

            //given
            var throwed = borrowsService.ReturnBorrow(borrowId);
            var expected = announcement;

            //then
            StringAssert.Contains(throwed, expected);
        }

        [TestMethod]
        public void BorrowReturned()
        {
            //when
            Borrow anotherBorrow = new BorrowBuilder()
            .SetId(1)
            .SetCustomerId(1)
            .SetBookId(1)
            .Build();

            var customerRepository = MockFactory.CreateCustomersRepository(new List<Customer>() { testCustomer });
            var borrowsRepository = MockFactory.CreateBorrowsRepository(new List<Borrow>() { anotherBorrow });
            var booksRepository = MockFactory.CreateBooksRepository(new List<Book>() { testBook });

            var borrowsService = new BorrowsService(customerRepository, borrowsRepository, booksRepository);

            //given
            var throwed = borrowsService.ReturnBorrow(1);
            var expected = $"Zwrócono, Tytuł: {testBook.Title} Klienta: {testCustomer.Name} {testCustomer.Surname}";

            //then
            StringAssert.Contains(throwed, expected);
        }
    }
}
