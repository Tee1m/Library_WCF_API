using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryService
{
    public class LibraryDbClient : IDatabaseClient
    {
        private LibraryDb _db = new LibraryDb();
        
        public void AddBook(Book book)
        {
            _db.Books.Add(book);
            _db.SaveChanges();
        }

        public void AddBorrow(Borrow borrow)
        {
            _db.Borrows.Add(borrow);
            _db.SaveChanges();
        }

        public void AddCustomer(Customer customer)
        {
            _db.Customers.Add(customer);
            _db.SaveChanges();
        }

        public List<Book> GetBooks()
        {
            return _db.Books.ToList();
        }

        public List<Borrow> GetBorrows()
        {
            return _db.Borrows.ToList();
        }

        public List<Customer> GetCustomers()
        {
            return _db.Customers.ToList();
        }

        public void RemoveBook(Book book)
        {
            _db.Books.Remove(book);
            _db.SaveChanges();
        }

        public void RemoveCustomer(Customer customer)
        {
            _db.Customers.Remove(customer);
            _db.SaveChanges();
        }

        public void Return(Borrow borrow)
        {
            _db.Entry(borrow).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();
        }

        public void ModifyBook(Book book)
        {
            _db.Entry(book).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();
        }
    }
}
