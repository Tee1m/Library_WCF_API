using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.IntegrationTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryHost;
using LibraryService;
using Autofac;

namespace CustomerServicesTests
{
    [TestClass]
    public class DeleteCustomerTests : TransactionIsolator
    {
        private static ICustomersService customersService;
        private static IBorrowsService borrowsService;
        private static IBooksService booksService;
        private CustomerDTO customer = new CustomerDTO();

        [ClassInitialize]
        public static void SetUpTests(TestContext testContext)
        {
            var container = ContainerIoC.RegisterContainerBuilder().Build();
            customersService = container.BeginLifetimeScope().Resolve<ICustomersService>();
            booksService = container.BeginLifetimeScope().Resolve<IBooksService>();
            borrowsService = container.BeginLifetimeScope().Resolve<IBorrowsService>();
        }

        [TestMethod]
        public void DeleteExistingCustomer()
        {
            //when
            customer.Name = "Maciej";
            customer.Surname = "Hanulak";
            customer.Address = "ul. Moja";
            customer.TelephoneNumber = "123456789";

            customersService.AddCustomer(customer);
            var query = customersService.GetCustomers();
            var customerId = query.Where(a => a.Name == customer.Name && a.Surname == customer.Surname && a.Address == customer.Address && a.TelephoneNumber == customer.TelephoneNumber).Select(a => a.Id).Single();
           
            //given
            customersService.DeleteCustomer(customerId);

            query = customersService.GetCustomers();

            //then
            Assert.IsTrue(query.Count() == 0);
        }

        [TestMethod]
        public void MessageOfDeletingExistingCustomer()
        {
            //when
            customer.Name = "Maciej";
            customer.Surname = "Hanulak";
            customer.Address = "ul. Moja";
            customer.TelephoneNumber = "123456789";
            
            customersService.AddCustomer(customer);
            
            //given
            var query = customersService.GetCustomers();
            var customerId = query.Where(a => a.Name == customer.Name && a.Surname == customer.Surname && a.Address == customer.Address && a.TelephoneNumber == customer.TelephoneNumber).Select(a => a.Id).Single();
            var message = customersService.DeleteCustomer(customerId);

            //then
            StringAssert.Contains(message, $"Usunięto Klienta, P. {customer.Name} {customer.Surname}.");
        }

        [TestMethod]
        public void NotExistingCustomerIsNotDeleted()
        {
            //when
            customer.Name = "Maciej";
            customer.Surname = "Hanulak";
            customer.Address = "ul. Moja";
            customer.TelephoneNumber = "123456789";

            //given
            var message = customersService.DeleteCustomer(3);

            //then
            StringAssert.Contains(message, "Nie znaleziono wskazanego Klienta w bazie biblioteki.");
        }

        [TestMethod]
        public void CustomerWithBorrowsIsNotDeleted()
        {
            //when 
            customer.Name = "Maciej";
            customer.Surname = "Hanulak";
            customer.Address = "ul. Moja";
            customer.TelephoneNumber = "123456789";

            BookDTO book = new BookDTO();

            book.Amount = 3;
            book.AuthorName = "Sztefan";
            book.AuthorSurname = "Szymoniak";
            book.Description = "hehe";
            book.Title = "Heheszki";

            booksService.AddBook(book);
            customersService.AddCustomer(customer);
            

            var customerQuery = customersService.GetCustomers();
            var bookQuery = booksService.GetBooks();
            var bookId = bookQuery.Where(a => a.Title == book.Title && a.AuthorName == book.AuthorName && a.AuthorSurname == book.AuthorSurname).Select(a => a.Id).First();
            var customerId = customerQuery.Where(a => a.Name == customer.Name && a.Surname == customer.Surname && a.Address == customer.Address && a.TelephoneNumber == customer.TelephoneNumber).Select(a => a.Id).First();
            borrowsService.AddBorrow(customerId, bookId);

            //given
            var message = customersService.DeleteCustomer(customerId);

            //then
            StringAssert.Contains(message, "Nie usunieto Klienta, ponieważ posiada on jeszcze wypożyczone książki.");
        }
    }
}
