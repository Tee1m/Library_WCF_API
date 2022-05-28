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

namespace BooksServiceTests
{
    [TestClass]
    public class DeleteBookTests : TransactionIsolator
    {
        private ICommandBus commandBus = new CommandBus();
        private DeleteBook deleteBook = new DeleteBook();


        //[TestMethod]
        //public void DeleteBook()
        //{
        //    //when
        //    book.AuthorName = "Test";
        //    book.AuthorSurname = "testName";
        //    book.Amount = 3;
        //    book.Description = "test test test";
        //    book.Title = "TestTitle";

        //    //given
        //    _booksService.AddBook(book);
        //    var books = _booksService.GetBooks();

        //    var BookId = books.Where(a => a.AuthorName == book.AuthorName && a.AuthorSurname == book.AuthorSurname && a.Title == book.Title).Select(a => a.Id).Single();
        //    _booksService.DeleteBook(BookId);
        //    books = _booksService.GetBooks();

        //    //then
        //    Assert.IsTrue(books.Count == 0);
        //}

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
            commandBus.Handle<CreateBook>(createBook);
            var throwed = commandBus.Handle<DeleteBook>(deleteBook);

            //then
            StringAssert.Contains(throwed, $"Usunięto książkę, Tytuł: {deleteBook.Title} Autor: {deleteBook.AuthorName} {deleteBook.AuthorSurname}.");
        }

        [TestMethod]
        public void NotExistedBookDidNotDeleted()
        {
            //when

            //given
            var throwed = commandBus.Handle<DeleteBook>(deleteBook);

            //then
            StringAssert.Contains(throwed, "Nie znaleziono wskazanej Książki w bazie biblioteki.");
        }

        //[TestMethod]
        //public void DidNotReturnedBookCanNotDeleted()
        //{
        //    //when
        //    book.AuthorName = "Test";
        //    book.AuthorSurname = "testName";
        //    book.Amount = 3;
        //    book.Description = "test test test";
        //    book.Title = "TestTitle";

        //    Customer customer = new Customer();

        //    customer.Name = "Maciej";
        //    customer.Surname = "Hanulak";
        //    customer.Address = "ul. Moja";
        //    customer.TelephoneNumber = "123456789";

        //    _booksService.AddBook(book);
        //    _customersService.AddCustomer(customer);

        //    var books = _booksService.GetBooks();
        //    var customers = _customersService.GetCustomers();

        //    var BookId = books.Where(a => a.AuthorName == book.AuthorName && a.AuthorSurname == book.AuthorSurname && a.Title == book.Title).Select(a => a.Id).Single();
        //    var CustomerId = customers.Where(a => a.Name == customer.Name && a.Surname == customer.Surname && a.Address == customer.Address && a.TelephoneNumber == customer.TelephoneNumber).Select(a => a.Id).Single();

        //    //given
        //    _borrowsService.AddBorrow(CustomerId, BookId);
        //    var message = _booksService.DeleteBook(BookId);

        //    //then
        //    StringAssert.Contains(message, "Nie usunieto książki, ponieważ nie wszystkie egzemplarze zostały zwrócone");
        //}
    }
}
