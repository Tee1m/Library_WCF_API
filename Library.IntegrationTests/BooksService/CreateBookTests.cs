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

namespace BookCommandsTests
{ 
    [TestClass]
    public class CreateBookTests : TransactionIsolator
    {
        private ICommandBus commandBus = new CommandBus();
        private IQueryBus queryBus = new QueryBus();
        private CreateBook command = new CreateBook();

        Book book = new Book();

        [TestMethod]
        public void AddNewBook()
        {
            //when
            //when
            command.AuthorName = "Test";
            command.AuthorSurname = "testName";
            command.Amount = 4;
            command.Title = "TestTitle";
            command.Description = "TestDesc";

            //given
            commandBus.Handle(command);
            var books = queryBus.Handle(new GetBooks()) as List<BookDTO>;

            //then
            Assert.IsTrue(books.Count == 1);
        }

        [TestMethod]
        public void CorrectMessageOfAddedBookOperation()
        {
            //when
            command.AuthorName = "Test";
            command.AuthorSurname = "testName";
            command.Amount = 4;
            command.Title = "TestTitle";
            command.Description = "TestDesc";

            //given
            var throwed = commandBus.Handle(command);

            //then
            StringAssert.Contains(throwed, "Dodano Książkę do bazy danych.");
        }

        [TestMethod]
        public void NullBookNotAdded()
        {
            //when
            command.AuthorName = null;
            command.AuthorSurname = "testName";
            command.Amount = 4;
            command.Title = "TestTitle";

            //given
            var throwed = commandBus.Handle(command);

            //then
            StringAssert.Contains(throwed, "Nie dodano książki, ponieważ conajmniej jedno z atrybutów nie zawiera wartości.");
        }

        [TestMethod]
        public void ExistedBookReplenishedQuantity()
        {
            //when
            command.AuthorName = "Test";
            command.AuthorSurname = "testName";
            command.Amount = 4;
            command.Title = "TestTitle";
            command.Description = "TestDesc";

            //given
            commandBus.Handle(command);
            var throwed = commandBus.Handle(command);

            //then
            StringAssert.Contains(throwed, "Książka znajduje się w bazie biblioteki. Uzupełniono jej dostępność.");
        }
    }
}
