using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.IntegrationTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryHost;
using Application;
using Autofac;
using Domain;

namespace CustomerCommandTests
{
    [TestClass]
    public class DeleteCustomerTests : TransactionIsolator
    {
        private ICommandBus _commandBus = new CommandBus();
        private IQueryBus _queryBus = new QueryBus();
        private DeleteCustomer _deleteComand = new DeleteCustomer();

        [TestMethod]
        public void DeleteExistingCustomer()
        {
            //when
            CreateCustomer createCommand = new CreateCustomer();
            createCommand.Name = "Maciej";
            createCommand.Surname = "Hanulak";
            createCommand.Address = "ul. Moja";
            createCommand.TelephoneNumber = "123456789";

            _deleteComand.TelephoneNumber = createCommand.TelephoneNumber;

            //given
            _commandBus.Handle(createCommand);
            _commandBus.Handle(_deleteComand);

            var query = _queryBus.Handle(new GetCustomers());

            //then
            Assert.IsTrue(query.Count() == 0);
        }

        [TestMethod]
        public void MessageOfDeletingExistingCustomer()
        {
            //when
            var createCommand = new CreateCustomer();

            _deleteComand.TelephoneNumber = "123456789";

            createCommand.Name = "Maciej";
            createCommand.Surname = "Hanulak";
            createCommand.Address = "ul. Moja";
            createCommand.TelephoneNumber = _deleteComand.TelephoneNumber;

            _commandBus.Handle(createCommand);

            //given
            var throwed = _commandBus.Handle(_deleteComand);

            //then
            StringAssert.Contains(throwed, "Usunięto Klienta");
        }

        [TestMethod]
        public void NotExistingCustomerIsNotDeleted()
        {
            //when
            _deleteComand.TelephoneNumber = "123456789";

            //given
            var throwed = _commandBus.Handle(_deleteComand);

            //then
            StringAssert.Contains(throwed, "Nie znaleziono wskazanego Klienta w bazie biblioteki.");
        }

        [TestMethod]
        public void CustomerWithBorrowsIsNotDeleted()
        {
            //when 
            var createBookCommand = new CreateBook();

            createBookCommand.AuthorName = "Test";
            createBookCommand.AuthorSurname = "testName";
            createBookCommand.Amount = 4;
            createBookCommand.Title = "TestTitle";
            createBookCommand.Description = "TestDesc";

            _commandBus.Handle(createBookCommand);

            var createCustomerCommand = new CreateCustomer();

            createCustomerCommand.Name = "Maciej";
            createCustomerCommand.Surname = "Hanulak";
            createCustomerCommand.Address = "ul. Moja";
            createCustomerCommand.TelephoneNumber = "123456789";

            _commandBus.Handle(createCustomerCommand);

            var customers = _queryBus.Handle(new GetCustomers()) as List<CustomerDTO>;
            var books = _queryBus.Handle(new GetBooks()) as List<BookDTO>;

            var customerId = customers.Where(x => x.TelephoneNumber == createCustomerCommand.TelephoneNumber)
                .Select(x => x.Id)
                .Single();
            var bookId = books.Where(x => x.Title == createBookCommand.Title && x.AuthorName == createBookCommand.AuthorName && x.AuthorSurname == createBookCommand.AuthorSurname)
                .Select(x => x.Id)
                .Single();

            //given
            CreateBorrow createBorrowCommand = new CreateBorrow();

            createBorrowCommand.BookId = bookId;
            createBorrowCommand.CustomerId = customerId;

            _commandBus.Handle(createBorrowCommand);

            DeleteCustomer deleteCustomer = new DeleteCustomer();
            deleteCustomer.TelephoneNumber = createCustomerCommand.TelephoneNumber;

            var throwed = _commandBus.Handle(deleteCustomer);

            //then
            StringAssert.Contains(throwed, "Nie usunieto Klienta, ponieważ posiada on jeszcze wypożyczone książki.");
        }
    }
}
