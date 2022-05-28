using Application;
using Autofac;
using Domain;
using Library.IntegrationTests;
using LibraryHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BorrowCommandTests
{
    [TestClass]
    public class ReturnBorrowTests : TransactionIsolator
    {
        private ICommandBus _commandBus = new CommandBus();
        private IQueryBus _queryBus = new QueryBus();

        [TestMethod]
        public void BorrowIsNotExist()
        {
            //when
            ReturnBorrow returnBorrowCommand = new ReturnBorrow();
            returnBorrowCommand.BorrowId = -1;

            //given
            var throwed = _commandBus.Handle(returnBorrowCommand);

            //then
            StringAssert.Contains(throwed, "Nie znaleziono wskazanego wypożyczenia w bazie biblioteki.");


        }

        [TestMethod]
        public void BorrowIsAlreadyReturned()
        {
            //when
            CreateBook createBookCommand = new CreateBook();
            createBookCommand.AuthorName = "Test";
            createBookCommand.AuthorSurname = "testName";
            createBookCommand.Amount = 1;
            createBookCommand.Title = "TestTitle";
            createBookCommand.Description = "TestDesc";

            CreateCustomer createCustomerCommand = new CreateCustomer();

            createCustomerCommand.Name = "Maciej";
            createCustomerCommand.Surname = "Hanulak";
            createCustomerCommand.Address = "ul. Moja";
            createCustomerCommand.TelephoneNumber = "123456789";

            _commandBus.Handle(createBookCommand);
            _commandBus.Handle(createCustomerCommand);

            var customers = _queryBus.Handle(new GetCustomers()) as List<CustomerDTO>;
            var books = _queryBus.Handle(new GetBooks()) as List<BookDTO>;

            var customerId = customers.Where(x => x.TelephoneNumber == createCustomerCommand.TelephoneNumber)
                          .Select(x => x.Id)
                          .Single();

            var bookId = books.Where(x => x.AuthorName == createBookCommand.AuthorName && x.AuthorSurname == createBookCommand.AuthorSurname && x.Title == createBookCommand.Title)
                              .Select(x => x.Id)
                              .Single();

            //given
            CreateBorrow createBorrowCommand = new CreateBorrow();
            createBorrowCommand.BookId = bookId;
            createBorrowCommand.CustomerId = customerId;

            _commandBus.Handle(createBorrowCommand);

            var borrows = _queryBus.Handle(new GetBorrows()) as List<BorrowDTO>;

            var borrowId = borrows.Where(x => x.CustomerId == customerId && x.BookId == bookId).Select(x => x.Id).Single();

            ReturnBorrow returnBorrowCommand = new ReturnBorrow();
            returnBorrowCommand.BorrowId = borrowId;

            _commandBus.Handle(returnBorrowCommand);
            var throwed = _commandBus.Handle(returnBorrowCommand);

            //then
            StringAssert.Contains(throwed, "Wypożyczenie zostało już zwrócone.");
        }

        [TestMethod]
        public void TheLoanHasBeenReturned()
        {
            //when
            CreateBook createBookCommand = new CreateBook();
            createBookCommand.AuthorName = "Test";
            createBookCommand.AuthorSurname = "testName";
            createBookCommand.Amount = 1;
            createBookCommand.Title = "TestTitle";
            createBookCommand.Description = "TestDesc";

            CreateCustomer createCustomerCommand = new CreateCustomer();

            createCustomerCommand.Name = "Maciej";
            createCustomerCommand.Surname = "Hanulak";
            createCustomerCommand.Address = "ul. Moja";
            createCustomerCommand.TelephoneNumber = "123456789";

            _commandBus.Handle(createBookCommand);
            _commandBus.Handle(createCustomerCommand);

            var customers = _queryBus.Handle(new GetCustomers()) as List<CustomerDTO>;
            var books = _queryBus.Handle(new GetBooks()) as List<BookDTO>;

            var customerId = customers.Where(x => x.TelephoneNumber == createCustomerCommand.TelephoneNumber)
                          .Select(x => x.Id)
                          .Single();

            var bookId = books.Where(x => x.AuthorName == createBookCommand.AuthorName && x.AuthorSurname == createBookCommand.AuthorSurname && x.Title == createBookCommand.Title)
                              .Select(x => x.Id)
                              .Single();

            //given
            CreateBorrow createBorrowCommand = new CreateBorrow();
            createBorrowCommand.BookId = bookId;
            createBorrowCommand.CustomerId = customerId;

            _commandBus.Handle(createBorrowCommand);

            var borrows = _queryBus.Handle(new GetBorrows()) as List<BorrowDTO>;

            var borrowId = borrows.Where(x => x.CustomerId == customerId && x.BookId == bookId).Select(x => x.Id).Single();

            ReturnBorrow returnBorrowCommand = new ReturnBorrow();
            returnBorrowCommand.BorrowId = borrowId;

            var throwed = _commandBus.Handle(returnBorrowCommand);

            //then
            StringAssert.Contains(throwed, $"Zwrócono, Tytuł: { createBookCommand.Title } Klienta: { createCustomerCommand.Name } { createCustomerCommand.Surname }");
        }
    }
}
