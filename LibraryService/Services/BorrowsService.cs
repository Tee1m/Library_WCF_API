using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.Entity;

namespace LibraryService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class BorrowsService : IBorrowsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BorrowsService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        

        public string AddBorrow(int customerId, int bookId)
        {
            var customersQuery = _unitOfWork.CustomersRepository.Get();
            var booksQuery = _unitOfWork.BooksRepository.Get();

            if (!customersQuery.Where(x => x.Id == customerId).Any())
            {
                return "Nie znaleziono wskazanego Klienta w bazie biblioteki.";
            }
            else if (!booksQuery.Where(x => x.Id == bookId).Any())
            {
                return "Nie znaleziono wskazanej Książki w bazie biblioteki.";
            }

            var customer = customersQuery.Where(x => x.Id == customerId).Single();
            var book = booksQuery.Where(x => x.Id == bookId).Single();

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

        public string ReturnBorrow(int id)
        {
            var borrowsQuery = _unitOfWork.BorrowsRepository.Get();

            if (!borrowsQuery.Where(x => x.Id == id).Any())
            {
                return "Nie znaleziono wskazanego wypożyczenia w bazie biblioteki.";
            }

            var borrow = borrowsQuery.Where(x => x.Id == id).Single(); 

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

        public List<BorrowDTO> GetBorrows()
        {   
            return _unitOfWork.BorrowsRepository.Get();
        }
    }
}
