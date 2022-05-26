using Domain;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace Application
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class BooksService : IBooksService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BooksService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public string AddBook(Book newBook)
        {
            if (BookIsNullable(newBook))
            {
                return "Nie dodano książki, ponieważ conajmniej jedno z atrybutów nie zawiera wartości.";
            }

            var books = _unitOfWork.BooksRepository.Get();

            foreach (var book in books)
            {
                if (IsSimilarBook(book, newBook))
                {
                     book.Amount += newBook.Amount;

                     _unitOfWork.BooksRepository.Update(book);

                     return "Książka znajduje się w bazie biblioteki. Uzupełniono jej dostępność.";
                }
            }

            _unitOfWork.BooksRepository.Add(newBook);
            _unitOfWork.Commit();

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
            var book = new Book();

            var books = _unitOfWork.BooksRepository.Get().ToList();
            var borrows = _unitOfWork.BorrowsRepository.Get().ToList();

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

            _unitOfWork.BooksRepository.Remove(book);
            _unitOfWork.Commit();

            return $"Usunięto książkę, Tytuł: {book.Title} Autor: {book.AuthorName} {book.AuthorSurname}.";
        }

        public List<Book> GetBooks()
        {
            return _unitOfWork.BooksRepository.Get().ToList();
        }
    }
}
