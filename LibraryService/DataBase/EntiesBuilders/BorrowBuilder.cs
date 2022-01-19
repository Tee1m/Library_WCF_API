using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryService.DataBase.EntiesBuilders
{
    class BorrowBuilder
    {
        private readonly Borrow _borrow = new Borrow();

        public BorrowBuilder SetId(int id)
        {
            _borrow.Id = id;
            return this;
        }

        public BorrowBuilder SetCustomer(Customer customer)
        {
            _borrow.Customer = customer;
            return this;
        }

        public BorrowBuilder SetCustomerId(int customerId)
        {
            _borrow.CustomerId = customerId;
            return this;
        }

        public BorrowBuilder SetBook(Book book)
        {
            _borrow.Book = book;
            return this;
        }

        public BorrowBuilder SetBookId(int bookId)
        {
            _borrow.BookId = bookId;
            return this;
        }

        public BorrowBuilder SetDateOfBorrow(DateTime date)
        {
            _borrow.DateOfBorrow = date;
            return this;
        }

        public BorrowBuilder SetReturnDate(DateTime returnDate)
        {
            _borrow.Return = returnDate;
            return this;
        }
    }
}
