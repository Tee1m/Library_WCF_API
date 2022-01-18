using System.Collections.Generic;
using System.Linq;

namespace LibraryService
{
    public class BooksRepository : IBooksRepository
    {
        private readonly IDatabaseClient _dbClient;

        public BooksRepository(IDatabaseClient dbClient)
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

            var books = _dbClient.GetBooks().Where(x => x.Id == id).ToList();
            var borrows = _dbClient.GetBorrows().ToList();

            if (books.Count() == 0)
            {
                return "Nie znaleziono wskazanej Książki w bazie biblioteki.";
            }
            else if (borrows == null)
            {
                if (borrows.Where(x => x.Book.Id == id && x.Return == null).Count() != 0)
                {
                    return "Nie usunieto książki, ponieważ nie wszystkie egzemplarze zostały zwrócone";
                }    
            }

            book = books[0];

            _dbClient.RemoveBook(book);

            return $"Usunięto książkę, Tytuł: {book.Title} Autor: {book.AuthorName} {book.AuthorSurname}.";
        }

        public List<Book> GetBooks()
        {
            return _dbClient.GetBooks();
        }
    }
}
