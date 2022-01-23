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
    public class AddBorrowTests
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

        [TestMethod]
        [DataRow(2, 1, "Nie znaleziono wskazanego Klienta w bazie biblioteki.")]
        [DataRow(1, 2, "Nie znaleziono wskazanej Książki w bazie biblioteki.")]
        [DataRow(1, 1, "Wpożyczenie jest niemożliwe, ponieważ książki niema na stanie biblioteki.")]
        public void ExceptionBorrowNotAdded(int customerId, int bookId, string announcement)
        {
            //when
            var dbClient = MockDataBaseClient(new List<Customer>() { testCustomer }, new List<Book>() { testBook });
            var borrowsService = new BorrowsService(dbClient);

            //given
            var throwed = borrowsService.AddBorrow(customerId, bookId);
            var expected = announcement;

            //then
            StringAssert.Contains(throwed, expected);
        }

        [TestMethod]
        public void BorrowAdded()
        {
            //when
            testBook.Availability = 1;
            var dbClient = MockDataBaseClient(new List<Customer>() { testCustomer }, new List<Book>() { testBook });
            var borrowsService = new BorrowsService(dbClient);

            //given
            var throwed = borrowsService.AddBorrow(1, 1);
            var expected = $"Wyporzyczono, Tytuł: {testBook.Title}, Klientowi: {testCustomer.Name} {testCustomer.Surname}";

            //then
            StringAssert.Contains(throwed, expected);
        }

        IDataBaseClient MockDataBaseClient(List<Customer> customersList, List<Book> booksList)
        {
            var mockDBClient = new Mock<IDataBaseClient>();

            mockDBClient.Setup(x => x.GetCustomers())
                .Returns(customersList);

            mockDBClient.Setup(x => x.GetBooks())
                .Returns(booksList);

            return mockDBClient.Object;
        }
    }
}
