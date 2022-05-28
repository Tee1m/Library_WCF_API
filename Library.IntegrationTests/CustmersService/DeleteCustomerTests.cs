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

namespace CustomerServicesTests
{
    [TestClass]
    public class DeleteCustomerTests : TransactionIsolator
    {
        private ICommandBus commandBus = new CommandBus();
        private DeleteCustomer deleteComand = new DeleteCustomer();

        //[TestMethod]
        //public void DeleteExistingCustomer()
        //{
        //    //when
        //    customer.Name = "Maciej";
        //    customer.Surname = "Hanulak";
        //    customer.Address = "ul. Moja";
        //    customer.TelephoneNumber = "123456789";

        //    customersService.AddCustomer(customer);
        //    var query = customersService.GetCustomers();
        //    var CustomerId = query.Where(a => a.Name == customer.Name && a.Surname == customer.Surname && a.Address == customer.Address && a.TelephoneNumber == customer.TelephoneNumber).Select(a => a.Id).Single();

        //    //given
        //    customersService.DeleteCustomer(CustomerId);

        //    query = customersService.GetCustomers();

        //    //then
        //    Assert.IsTrue(query.Count() == 0);
        //}

        [TestMethod]
        public void MessageOfDeletingExistingCustomer()
        {
            //when
            var createCommand = new CreateCustomer();

            deleteComand.TelephoneNumber = "123456789";

            createCommand.Name = "Maciej";
            createCommand.Surname = "Hanulak";
            createCommand.Address = "ul. Moja";
            createCommand.TelephoneNumber = deleteComand.TelephoneNumber;

            commandBus.Handle<CreateCustomer>(createCommand);

            //given
            var throwed = commandBus.Handle<DeleteCustomer>(deleteComand);

            //then
            StringAssert.Contains(throwed, "Usunięto Klienta");
        }

        [TestMethod]
        public void NotExistingCustomerIsNotDeleted()
        {
            //when
            deleteComand.TelephoneNumber = "123456789";

            //given
            var throwed = commandBus.Handle<DeleteCustomer>(deleteComand);

            //then
            StringAssert.Contains(throwed, "Nie znaleziono wskazanego Klienta w bazie biblioteki.");
        }

        //[TestMethod]
        //public void CustomerWithBorrowsIsNotDeleted()
        //{
        //    //when 
        //    customer.Name = "Maciej";
        //    customer.Surname = "Hanulak";
        //    customer.Address = "ul. Moja";
        //    customer.TelephoneNumber = "123456789";

        //    Book book = new Book();

        //    book.Amount = 3;
        //    book.AuthorName = "Sztefan";
        //    book.AuthorSurname = "Szymoniak";
        //    book.Description = "hehe";
        //    book.Title = "Heheszki";

        //    booksService.AddBook(book);
        //    customersService.AddCustomer(customer);


        //    var customerQuery = customersService.GetCustomers();
        //    var bookQuery = booksService.GetBooks();
        //    var BookId = bookQuery.Where(a => a.Title == book.Title && a.AuthorName == book.AuthorName && a.AuthorSurname == book.AuthorSurname).Select(a => a.Id).First();
        //    var CustomerId = customerQuery.Where(a => a.Name == customer.Name && a.Surname == customer.Surname && a.Address == customer.Address && a.TelephoneNumber == customer.TelephoneNumber).Select(a => a.Id).First();
        //    borrowsService.AddBorrow(CustomerId, BookId);

        //    //given
        //    var message = customersService.DeleteCustomer(CustomerId);

        //    //then
        //    StringAssert.Contains(message, "Nie usunieto Klienta, ponieważ posiada on jeszcze wypożyczone książki.");
        //}
    }
}
