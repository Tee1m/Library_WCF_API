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
    public class CreateBorrowTests : TransactionIsolator
    {
        private ICommandBus _commandBus = new CommandBus();
        private IQueryBus _queryBus = new QueryBus();

        [TestMethod]
        public void WhenBookBookIsOutOfStockBorrowNotAdded()
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
            var throwed = _commandBus.Handle(createBorrowCommand);

            //then
            StringAssert.Contains(throwed, "Wpożyczenie jest niemożliwe, ponieważ książki niema na stanie biblioteki.");
        }

        [TestMethod]
        public void WhenCustomerNotFoundBorrowNotAdded()
        {
            //when
            CreateBook createBookCommand = new CreateBook();
            createBookCommand.AuthorName = "Test";
            createBookCommand.AuthorSurname = "testName";
            createBookCommand.Amount = 1;
            createBookCommand.Title = "TestTitle";
            createBookCommand.Description = "TestDesc";

            _commandBus.Handle(createBookCommand);

            var books = _queryBus.Handle(new GetBooks()) as List<BookDTO>;

            var bookId = books.Where(x => x.AuthorName == createBookCommand.AuthorName && x.AuthorSurname == createBookCommand.AuthorSurname && x.Title == createBookCommand.Title)
                              .Select(x => x.Id)
                              .Single();
            //given 
            CreateBorrow createBorrowCommand = new CreateBorrow();
            createBorrowCommand.BookId = bookId;
            createBorrowCommand.CustomerId = -1;

            var throwed = _commandBus.Handle(createBorrowCommand);

            //then
            StringAssert.Contains(throwed, "Nie znaleziono wskazanego Klienta w bazie biblioteki.");
        }

        [TestMethod]
        public void WhenBookNotFoundBorrowNotAdded()
        {
            //when
            CreateCustomer createCustomerCommand = new CreateCustomer();

            createCustomerCommand.Name = "Maciej";
            createCustomerCommand.Surname = "Hanulak";
            createCustomerCommand.Address = "ul. Moja";
            createCustomerCommand.TelephoneNumber = "123456789";

            _commandBus.Handle(createCustomerCommand);

            var customers = _queryBus.Handle(new GetCustomers()) as List<CustomerDTO>;
            var customerId = customers.Where(x => x.TelephoneNumber == createCustomerCommand.TelephoneNumber)
                                      .Select(x => x.Id)
                                      .Single();
            //given 
            CreateBorrow createBorrowCommand = new CreateBorrow();
            createBorrowCommand.BookId = -1;
            createBorrowCommand.CustomerId = customerId;

            var throwed = _commandBus.Handle(createBorrowCommand);

            //then
            StringAssert.Contains(throwed, "Nie znaleziono wskazanej Książki w bazie biblioteki.");
        }

        [TestMethod]
        public void BorrowAdded()
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

            _commandBus.Handle(createCustomerCommand);
            _commandBus.Handle(createBookCommand);

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

            var throwed = _commandBus.Handle(createBorrowCommand);
            var expected = $"Wyporzyczono, Tytuł: {createBookCommand.Title}, Klientowi: {createCustomerCommand.Name} {createCustomerCommand.Surname}";

            //then
            StringAssert.Contains(throwed, expected);
        }
    }
}
