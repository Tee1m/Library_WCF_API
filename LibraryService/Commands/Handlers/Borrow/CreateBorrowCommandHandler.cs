using System.Linq;

namespace Application
{
    public class CreateBorrowCommandHandler : ICommandHandler<CreateBorrow>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateBorrowCommandHandler(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public string Handle(CreateBorrow command)
        {

            var customersQuery = _unitOfWork.CustomersRepository.Get();
            var booksQuery = _unitOfWork.BooksRepository.Get();

            if (!customersQuery.Where(x => x.Id == command.CustomerId).Any())
            {
                return "Nie znaleziono wskazanego Klienta w bazie biblioteki.";
            }
            else if (!booksQuery.Where(x => x.Id == command.BookId).Any())
            {
                return "Nie znaleziono wskazanej Książki w bazie biblioteki.";
            }

            var customer = customersQuery.Where(x => x.Id == command.CustomerId).Single();
            var book = booksQuery.Where(x => x.Id == command.BookId).Single();

            if (book.Amount == 0)
            {
                return "Wpożyczenie jest niemożliwe, ponieważ książki niema na stanie biblioteki.";
            }

            book.Amount--;

            _unitOfWork.BooksRepository.Update(book);
            _unitOfWork.BorrowsRepository.Add(customer, book);
            _unitOfWork.Commit();

            return $"Wyporzyczono, Tytuł: {book.Title}, Klientowi: {customer.Name} {customer.Surname}";
        }
    }
}
