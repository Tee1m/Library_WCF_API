using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryService
{
    public class BorrowDTOBuilder
    {
        private readonly BorrowDTO _borrow = new BorrowDTO();

        public BorrowDTOBuilder SetId(int id)
        {
            _borrow.Id = id;
            return this;
        }

        public BorrowDTOBuilder SetCustomer(string customer)
        {
            _borrow.Customer = customer;
            return this;
        }

        public BorrowDTOBuilder SetCustomerId(int customerId)
        {
            _borrow.CustomerId = customerId;
            return this;
        }

        public BorrowDTOBuilder SetBook(string book)
        {
            _borrow.Book = book;
            return this;
        }

        public BorrowDTOBuilder SetBookId(int bookId)
        {
            _borrow.BookId = bookId;
            return this;
        }

        public BorrowDTOBuilder SetDateOfBorrow(DateTime date)
        {
            _borrow.DateOfBorrow = date;
            return this;
        }

        public BorrowDTOBuilder SetReturnDate(DateTime returnDate)
        {
            _borrow.Return = returnDate;
            return this;
        }

        public BorrowDTO Build()
        {
            return _borrow;
        }
    }
}
