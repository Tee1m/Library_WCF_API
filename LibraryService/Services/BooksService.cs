using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace LibraryService
{
    public class BooksService : IBooksService
    {
        private readonly IBooksRepository _booksRepository;
        private readonly IBorrowsRepository _borrowsRepository;

        public BooksService(IBooksRepository booksRepository, IBorrowsRepository borrowsRepository)
        {
            this._booksRepository = booksRepository;
            this._borrowsRepository = borrowsRepository;
        }

        public string AddBook(BookDTO newBook)
        {
            if (BookIsNullable(newBook))
            {
                return "Nie dodano książki, ponieważ conajmniej jedno z atrybutów nie zawiera wartości.";
            }

            var books = _booksRepository.Get();

            foreach (var book in books)
            {
                if (IsSimilarBook(book, newBook))
                {
                     book.Amount += newBook.Amount;

                     _booksRepository.Attach(book);

                     return "Książka znajduje się w bazie biblioteki. Uzupełniono jej dostępność.";
                }
            }

            _booksRepository.Add(newBook);

            return "Dodano Książkę do bazy danych.";
        }

        private bool IsSimilarBook(BookDTO existing, BookDTO created)
        {
            return existing.Title.Contains(created.Title) && existing.AuthorName.Contains(created.AuthorName)
                && existing.AuthorSurname.Contains(created.AuthorSurname);
        }

        private bool BookIsNullable(BookDTO book)
        {
            return book.AuthorName == null || book.AuthorSurname == null || book.Description == "" || book.Title == null;
        }

        public string DeleteBook(int id)
        {
            var book = new BookDTO();

            var books = _booksRepository.Get().ToList();
            var borrows = _borrowsRepository.Get().ToList();

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

            _booksRepository.Remove(book);

            return $"Usunięto książkę, Tytuł: {book.Title} Autor: {book.AuthorName} {book.AuthorSurname}.";
        }

        public List<BookDTO> GetBooks()
        {
            return _booksRepository.Get().ToList();
        }
    }
}
