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
        private readonly IBookUniquenessChecker _bookUniquenessChecker;
        private IBusinessRule _rule;

        public BooksService(IUnitOfWork unitOfWork, IBookUniquenessChecker bookUniquenessChecker)
        {
            this._unitOfWork = unitOfWork;
            this._bookUniquenessChecker = bookUniquenessChecker;
        }

        public string AddBook(Book newBook)
        {
            _rule = newBook.HasAllValues();

            if (_rule.NotValid())
                return _rule.Message;

            _rule = newBook.IsUnique(_bookUniquenessChecker);

            if (_rule.NotValid())
                return _rule.Message;

            _unitOfWork.BooksRepository.Add(newBook);
            _unitOfWork.Commit();

            return "Dodano Książkę do bazy danych.";
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
