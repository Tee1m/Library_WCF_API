using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.Entity;

namespace LibraryService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class BorrowsService : IBorrowsService
    {
        private readonly IDatabaseClient _dbClient;

        public BorrowsService(IDatabaseClient dbClient)
        {
            this._dbClient = dbClient;
        }

        public string Borrow(int customerId, int bookId)
        {
            Customer customer = new Customer();
            Book book = new Book();

            var customersQuery = _dbClient.GetCustomers().Where(x => x.Id == customerId).ToList();
            var booksQuery = _dbClient.GetBooks().Where(x => x.Id == bookId).ToList();

            if (customersQuery.Count() == 0)
            {
                return "Nie znaleziono wskazanego Klienta w bazie biblioteki.";
            }
            else if (booksQuery.Count() == 0)
            {
                return "Nie znaleziono wskazanej Książki w bazie biblioteki.";
            }
            else if (booksQuery[0].Availability == 0)
            {
                return "Wpożyczenie jest niemożliwe, ponieważ książki niema na stanie biblioteki.";
            }

            customer = customersQuery[0];
            book = booksQuery[0];

            book.Availability--;

            Borrow borrow = new Borrow(customer, book);

            _dbClient.AddBorrow(borrow);

            return $"Wyporzyczono, Tytuł: {book.Title}, Klientowi: {customer.Name} {customer.Surname}";
        }

        public string Return(int id)
        {
            Customer customer = new Customer();
            Book book = new Book();
            Borrow borrow = new Borrow();

            var borrowsQuery = _dbClient.GetBorrows().Where(x => x.Id == id).ToList();

            if (borrowsQuery.Count() == 0)
            {
                return "Nie znaleziono wskazanego wypożyczenia w bazie biblioteki.";
            }
            else if (borrowsQuery[0].Return != null)
            {
                return "Wypożyczenie zostało już zwrócone.";
            }

            borrow = borrowsQuery[0];

            var customerQuery = _dbClient.GetCustomers().Where(x => x.Id == borrow.CustomerId).ToList();
            var bookQuery = _dbClient.GetBooks().Where(x => x.Id == borrow.BookId).ToList();

            customer = customerQuery[0];
            book = bookQuery[0];

            book.Availability++;
            borrow.Return = DateTime.Now;

            _dbClient.Return(borrow);

            return $"Zwrócono, Tytuł: {book.Title} Klienta: {customer.Name} {customer.Surname}";
        }

        public List<BorrowDTO> GetBorrows()
        {   
            List<BorrowDTO> borrowsDTO = new List<BorrowDTO>();
            BorrowDTO bDTO;

            var borrows = _dbClient.GetBorrows();

            foreach (var borrow in borrows)
            {
                bDTO = new BorrowDTO();

                var customersQuery = _dbClient.GetCustomers().Where(x => x.Id == borrow.CustomerId).ToList();
                var booksQuery = _dbClient.GetBooks().Where(x => x.Id == borrow.BookId).ToList();

                bDTO.Id = borrow.Id;
                bDTO.DateOfBorrow = borrow.DateOfBorrow;
                bDTO.Return = borrow.Return;
                bDTO.Customer = $"{customersQuery[0].Name} {customersQuery[0].Surname} {customersQuery[0].TelephoneNumber}";
                bDTO.Book = $"{booksQuery[0].Title}, {booksQuery[0].AuthorName} {booksQuery[0].AuthorSurname}";
                    
                borrowsDTO.Add(bDTO);
            }

            return borrowsDTO;
        }
    }
}
