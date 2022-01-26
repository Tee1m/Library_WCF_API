using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.Entity;

namespace LibraryService
{
    public class BorrowsService : IBorrowsService
    {
        private readonly ICustomersRepository _customersRepository;
        private readonly IBorrowsRepository _borrowsRepository;
        private readonly IBooksRepository _booksRepository;

        public BorrowsService(ICustomersRepository customers, IBorrowsRepository borrows, IBooksRepository books)
        {
            this._customersRepository = customers;
            this._borrowsRepository = borrows;
            this._booksRepository = books;
        }

        public string AddBorrow(int customerId, int bookId)
        {
            var customersQuery = _customersRepository.Get();
            var booksQuery = _booksRepository.Get();

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

            _borrowsRepository.Add(customer, book);

            return $"Wyporzyczono, Tytuł: {book.Title}, Klientowi: {customer.Name} {customer.Surname}";
        }

        public string ReturnBorrow(int id)
        {
            var borrowsQuery = _borrowsRepository.Get();

            if (!borrowsQuery.Where(x => x.Id == id).Any())
            {
                return "Nie znaleziono wskazanego wypożyczenia w bazie biblioteki.";
            }

            var borrow = borrowsQuery.Where(x => x.Id == id).Single(); 

            if (borrow.Return != null)
            {
                return "Wypożyczenie zostało już zwrócone.";
            }

            var customer = _customersRepository.Get().Where(x => x.Id == borrow.CustomerId).Single();
            var book = _booksRepository.Get().Where(x => x.Id == borrow.BookId).Single();

            book.Amount++;
            borrow.Return = DateTime.Now;

            _borrowsRepository.Attach(borrow);

            return $"Zwrócono, Tytuł: {book.Title} Klienta: {customer.Name} {customer.Surname}";
        }

        public List<BorrowDTO> GetBorrows()
        {   
            return _borrowsRepository.Get();
        }
    }
}
