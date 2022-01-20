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

        public string AddBorrow(int customerId, int bookId)
        {
            var customersQuery = _dbClient.GetCustomers();
            var booksQuery = _dbClient.GetBooks();

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

            if (book.Availability == 0)
            {
                return "Wpożyczenie jest niemożliwe, ponieważ książki niema na stanie biblioteki.";
            }

            book.Availability--;

            Borrow borrow = new Borrow(customer, book);

            _dbClient.AddBorrow(borrow);

            return $"Wyporzyczono, Tytuł: {book.Title}, Klientowi: {customer.Name} {customer.Surname}";
        }

        public string ReturnBorrow(int id)
        {
            var borrowsQuery = _dbClient.GetBorrows();

            if (!borrowsQuery.Where(x => x.Id == id).Any())
            {
                return "Nie znaleziono wskazanego wypożyczenia w bazie biblioteki.";
            }

            var borrow = borrowsQuery.Where(x => x.Id == id).Single(); 

            if (borrow.Return != null)
            {
                return "Wypożyczenie zostało już zwrócone.";
            }

            var customer = _dbClient.GetCustomers().Where(x => x.Id == borrow.CustomerId).Single();
            var book = _dbClient.GetBooks().Where(x => x.Id == borrow.BookId).Single();

            book.Availability++;
            borrow.Return = DateTime.Now;

            _dbClient.ReturnBorrow(borrow);

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

                var customer = _dbClient.GetCustomers().Where(x => x.Id == borrow.CustomerId).Single();
                var book = _dbClient.GetBooks().Where(x => x.Id == borrow.BookId).Single();

                bDTO.Id = borrow.Id;
                bDTO.DateOfBorrow = borrow.DateOfBorrow;
                bDTO.Return = borrow.Return;
                bDTO.Customer = customer.ToString();
                bDTO.Book = book.ToString();
                    
                borrowsDTO.Add(bDTO);
            }

            return borrowsDTO;
        }
    }
}
