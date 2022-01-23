using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryService;
using Moq;

namespace BorrowServiceTests
{
    [TestClass]
    public class ReturnBorrowTests
    {
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
            var dbClient = MockDataBaseClient(new List<Customer>() { testCustomer }, new List<Book>() { testBook }
                    , new List<Borrow>() { testBorrow });
            var borrowsService = new BorrowsService(dbClient);

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

            var dbClient = MockDataBaseClient(new List<Customer>() { testCustomer }, new List<Book>() { testBook }
                    , new List<Borrow>() { anotherBorrow });
            var borrowsService = new BorrowsService(dbClient);

            //given
            var throwed = borrowsService.ReturnBorrow(1);
            var expected = $"Zwrócono, Tytuł: {testBook.Title} Klienta: {testCustomer.Name} {testCustomer.Surname}";

            //then
            StringAssert.Contains(throwed, expected);
        }

        IDataBaseClient MockDataBaseClient(List<Customer> customersList, List<Book> booksList, List<Borrow> borrowsList)
        {
            var mockDBClient = new Mock<IDataBaseClient>();

            mockDBClient.Setup(x => x.GetCustomers())
                .Returns(customersList);

            mockDBClient.Setup(x => x.GetBorrows())
                .Returns(borrowsList);

            mockDBClient.Setup(x => x.GetBooks())
                .Returns(booksList);

            return mockDBClient.Object;
        }
    }
}
