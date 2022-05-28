//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Application;
//using Moq;
//using Domain;
//using Tests.Common;

//namespace ApplicationTests
//{
//    [TestClass]
//    public class AddBorrowTests
//    {
//        Customer testCustomer = new CustomerBuilder()
//            .SetId(1)
//            .SetName("Test")
//            .SetSurname("Test")
//            .SetAddress("Test")
//            .SetTelephoneNumber("123")
//            .Build();

//        Book testBook = new BookBuilder()
//            .SetId(1)
//            .SetTitle("Test")
//            .SetAuthorName("Test")
//            .SetAuthorSurname("Test")
//            .SetAvailability(0)
//            .SetDescription("Test")
//            .Build();

//        [TestMethod]
//        [DataRow(2, 1, "Nie znaleziono wskazanego Klienta w bazie biblioteki.")]
//        [DataRow(1, 2, "Nie znaleziono wskazanej Książki w bazie biblioteki.")]
//        [DataRow(1, 1, "Wpożyczenie jest niemożliwe, ponieważ książki niema na stanie biblioteki.")]
//        public void ExceptionBorrowNotAdded(int customerId, int bookId, string announcement)
//        {
//            //when
//            var unitOfWork = new Mock<IUnitOfWork>();
//            unitOfWork.Setup(a => a.CustomersRepository.Get()).Returns(new List<Customer>() { testCustomer });
//            unitOfWork.Setup(a => a.BorrowsRepository.Get()).Returns(new List<Borrow>());
//            unitOfWork.Setup(a => a.BooksRepository.Get()).Returns(new List<Book>() { testBook });

//            var borrowsService = new BorrowsService(unitOfWork.Object);

//            //given
//            var throwed = borrowsService.AddBorrow(customerId, bookId);
//            var expected = announcement;

//            //then
//            StringAssert.Contains(throwed, expected);
//        }

//        [TestMethod]
//        public void BorrowAdded()
//        {
//            //when
//            testBook.Amount = 1;
//            var unitOfWork = new Mock<IUnitOfWork>();
//            unitOfWork.Setup(a => a.CustomersRepository.Get()).Returns(new List<Customer>() { testCustomer });
//            unitOfWork.Setup(a => a.BorrowsRepository.Get()).Returns(new List<Borrow>());
//            unitOfWork.Setup(a => a.BooksRepository.Get()).Returns(new List<Book>() { testBook });

//            var borrowsService = new BorrowsService(unitOfWork.Object);

//            //given
//            var throwed = borrowsService.AddBorrow(1, 1);
//            var expected = $"Wyporzyczono, Tytuł: {testBook.Title}, Klientowi: {testCustomer.Name} {testCustomer.Surname}";

//            //then
//            StringAssert.Contains(throwed, expected);
//        }
//    }
//}
