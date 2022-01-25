using System.Collections.Generic;
using LibraryService;
using Moq;

namespace Library.ServicesTests
{
    class MockFactory
    {
        public static ICustomersRepository CreateCustomersRepository(List<Customer> customerList)
        {
            var mockCustomersRepository = new Mock<ICustomersRepository>();

            mockCustomersRepository.Setup(x => x.Get())
                .Returns(customerList);

            return mockCustomersRepository.Object;
        }

        public static IBorrowsRepository CreateBorrowsRepository(List<Borrow> borrowsList)
        {
            var mockBorrowsRepository = new Mock<IBorrowsRepository>();

            mockBorrowsRepository.Setup(x => x.Get())
                .Returns(borrowsList);

            return mockBorrowsRepository.Object;
        }

        public static IBooksRepository CreateBooksRepository(List<Book> booksList)
        {
            var mockBooksRepository = new Mock<IBooksRepository>();

            mockBooksRepository.Setup(x => x.Get())
                .Returns(booksList);

            return mockBooksRepository.Object;
        }
    }
}
