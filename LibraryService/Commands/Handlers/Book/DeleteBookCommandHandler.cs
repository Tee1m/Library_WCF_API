using Domain;
using System.Linq;

namespace Application
{
    public class DeleteBookCommandHandler : ICommandHandler<DeleteBook>
    {
        private readonly IUnitOfWork _unitOfWork;
        //private IBusinessRule _rule;

        public DeleteBookCommandHandler(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public string Handle(DeleteBook command)
        {
            var book = new Book();

            var books = _unitOfWork.BooksRepository.Get().ToList();
            var borrows = _unitOfWork.BorrowsRepository.Get().ToList();

            if (!books.Where(x => x.AuthorName == command.AuthorName && x.AuthorSurname == command.AuthorSurname && x.Title == command.Title).Any())
            {
                return "Nie znaleziono wskazanej Książki w bazie biblioteki.";
            }

            book = books.Where(x => x.AuthorName == command.AuthorName && x.AuthorSurname == command.AuthorSurname && x.Title == command.Title).Single();

            if (borrows != null)
            {
                if (borrows.Where(x => x.BookId == book.Id && x.Return == null).Any())
                {
                    return "Nie usunieto książki, ponieważ nie wszystkie egzemplarze zostały zwrócone";
                }
            }

            _unitOfWork.BooksRepository.Remove(book);
            _unitOfWork.Commit();

            return $"Usunięto książkę, Tytuł: {book.Title} Autor: {book.AuthorName} {book.AuthorSurname}.";
        }
    }


}
