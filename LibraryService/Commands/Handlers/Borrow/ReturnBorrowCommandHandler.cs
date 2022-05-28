using System;
using System.Linq;

namespace Application
{
    public class ReturnBorrowCommandHandler : ICommandHandler<ReturnBorrow>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReturnBorrowCommandHandler(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public string Handle(ReturnBorrow command)
        {
            var borrowsQuery = _unitOfWork.BorrowsRepository.Get();

            if (!borrowsQuery.Where(x => x.Id == command.BorrowId).Any())
            {
                return "Nie znaleziono wskazanego wypożyczenia w bazie biblioteki.";
            }

            var borrow = borrowsQuery.Where(x => x.Id == command.BorrowId).Single();

            if (borrow.Return != null)
            {
                return "Wypożyczenie zostało już zwrócone.";
            }

            var customer = _unitOfWork.CustomersRepository.Get().Where(x => x.Id == borrow.CustomerId).Single();
            var book = _unitOfWork.BooksRepository.Get().Where(x => x.Id == borrow.BookId).Single();

            book.Amount++;
            borrow.Return = DateTime.Now;

            _unitOfWork.BorrowsRepository.Update(borrow);
            _unitOfWork.Commit();

            return $"Zwrócono, Tytuł: {book.Title} Klienta: {customer.Name} {customer.Surname}";
        }
    }
}
