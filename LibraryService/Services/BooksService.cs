using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace LibraryService
{
    //[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class BooksService : IBooksService
    {
        private readonly IDataBaseClient _dbClient;

        public BooksService(IDataBaseClient dbClient)
        {
            this._dbClient = dbClient;
        }

        public string AddBook(Book newBook)
        {
            if (BookIsNullable(newBook))
            {
                return "Nie dodano książki, ponieważ conajmniej jedno z atrybutów nie zawiera wartości.";
            }

            var books = _dbClient.GetBooks();

            foreach (var book in books)
            {
                if (IsSimilarBook(book, newBook))
                {
                     book.Availability += newBook.Availability;

                     _dbClient.ModifyBook(book);

                     return "Książka znajduje się w bazie biblioteki. Uzupełniono jej dostępność.";
                }
            }

            _dbClient.AddBook(newBook);

            return "Dodano Książkę do bazy danych.";
        }

        private bool IsSimilarBook(Book existing, Book created)
        {
            return existing.Title.Contains(created.Title) && existing.AuthorName.Contains(created.AuthorName)
                && existing.AuthorSurname.Contains(created.AuthorSurname);
        }

        private bool BookIsNullable(Book book)
        {
            return book.AuthorName == null || book.AuthorSurname == null || book.Description == "" || book.Title == null;
        }

        public string DeleteBook(int id)
        {
            Book book = new Book();

            var books = _dbClient.GetBooks();
            var borrows = _dbClient.GetBorrows();

            if (!books.Where(x => x.Id == id).Any())
            {
                return "Nie znaleziono wskazanej Książki w bazie biblioteki.";
            }
            else if (borrows != null)
            {
                if (borrows.Where(x => x.BookId == id && x.Return == null).Any())
                {
                    return "Nie usunieto książki, ponieważ nie wszystkie egzemplarze zostały zwrócone";
                }    
            }

            book = books.Where(x => x.Id == id).Single();

            _dbClient.RemoveBook(book);

            return $"Usunięto książkę, Tytuł: {book.Title} Autor: {book.AuthorName} {book.AuthorSurname}.";
        }

        public List<Book> GetBooks()
        {
            return _dbClient.GetBooks();
        }
    }
}
