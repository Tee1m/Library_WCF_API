using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.IntegrationTests;
using LibraryHost;
using Application;
using Autofac;
using Domain;

namespace BookCommandsTests
{
    [TestClass]
    public class DeleteBookTests : TransactionIsolator
    {
        private ICommandBus _commandBus = new CommandBus();
        private IQueryBus _queryBus = new QueryBus();
        private DeleteBook deleteBook = new DeleteBook();


        [TestMethod]
        public void DeleteBook()
        {
            //when
            CreateBook createBookCommand = new CreateBook();
            createBookCommand.AuthorName = "Test";
            createBookCommand.AuthorSurname = "testName";
            createBookCommand.Amount = 4;
            createBookCommand.Title = "TestTitle";
            createBookCommand.Description = "TestDesc";

            _commandBus.Handle(createBookCommand);

            //given
            DeleteBook deleteBookCommand = new DeleteBook();
            deleteBookCommand.AuthorName = createBookCommand.AuthorName;
            deleteBookCommand.AuthorSurname = createBookCommand.AuthorSurname;
            deleteBookCommand.Title = createBookCommand.Title;

            _commandBus.Handle(deleteBookCommand);

            var books = _queryBus.Handle(new GetBooks()) as List<BookDTO>;

            //then
            Assert.IsTrue(books.Count == 0);
        }

        [TestMethod]
        public void CorrectMessageOfDeletingBookOperation()
        {
            //when
            CreateBook createBook = new CreateBook();
            createBook.AuthorName = "Test";
            createBook.AuthorSurname = "testName";
            createBook.Amount = 4;
            createBook.Title = "TestTitle";
            createBook.Description = "TestDesc";


            deleteBook.AuthorName = "Test";
            deleteBook.AuthorSurname = "testName";
            deleteBook.Title = "TestTitle";

            //given
            _commandBus.Handle(createBook);
            var throwed = _commandBus.Handle(deleteBook);

            //then
            StringAssert.Contains(throwed, $"Usunięto książkę, Tytuł: {deleteBook.Title} Autor: {deleteBook.AuthorName} {deleteBook.AuthorSurname}.");
        }

        [TestMethod]
        public void NotExistedBookDidNotDeleted()
        {
            //when

            //given
            var throwed = _commandBus.Handle(deleteBook);

            //then
            StringAssert.Contains(throwed, "Nie znaleziono wskazanej Książki w bazie biblioteki.");
        }

        [TestMethod]
        public void DidNotReturnedBookCanNotDeleted()
        {
            //when
            CreateBook createBookCommand = new CreateBook();
            createBookCommand.AuthorName = "Test";
            createBookCommand.AuthorSurname = "testName";
            createBookCommand.Amount = 4;
            createBookCommand.Title = "TestTitle";
            createBookCommand.Description = "TestDesc";

            CreateCustomer createCustomerCommand = new CreateCustomer();
            createCustomerCommand.Name = "Maciej";
            createCustomerCommand.Surname = "Hanulak";
            createCustomerCommand.Address = "ul. Moja";
            createCustomerCommand.TelephoneNumber = "123456789";

            _commandBus.Handle(createCustomerCommand);
            _commandBus.Handle(createBookCommand);

            var books = _queryBus.Handle(new GetBooks()) as List<BookDTO>;
            var customers = _queryBus.Handle(new GetCustomers()) as List<CustomerDTO>;

            var bookId = books.Where(x => x.AuthorName == createBookCommand.AuthorName && x.AuthorSurname == createBookCommand.AuthorSurname && x.Title == createBookCommand.Title)
                               .Select(x => x.Id)
                               .Single();

            var customerId = customers.Where(x => x.TelephoneNumber == createCustomerCommand.TelephoneNumber)
                                      .Select(x => x.Id)
                                      .Single();

            CreateBorrow createBorrowCommand = new CreateBorrow();
            createBorrowCommand.BookId = bookId;
            createBorrowCommand.CustomerId = customerId;

            _commandBus.Handle(createBorrowCommand);

            //given
            DeleteBook deleteCustomerCommand = new DeleteBook();
            deleteCustomerCommand.AuthorName = createBookCommand.AuthorName;
            deleteCustomerCommand.AuthorSurname = createBookCommand.AuthorSurname;
            deleteCustomerCommand.Title = createBookCommand.Title;

            var throwed = _commandBus.Handle(deleteCustomerCommand);

            //then
            StringAssert.Contains(throwed, "Nie usunieto książki, ponieważ nie wszystkie egzemplarze zostały zwrócone");
        }
    }
}
