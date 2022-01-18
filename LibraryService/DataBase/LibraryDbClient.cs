using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryService
{
    public class LibraryDbClient : IDatabaseClient
    {
        private readonly LibraryDb _dataBase;

        public LibraryDbClient(LibraryDb database)
        {
            this._dataBase = database;
        }
        
        public void AddBook(Book book)
        {
            _dataBase.Books.Add(book);
            _dataBase.SaveChanges();
        }

        public void AddBorrow(Borrow borrow)
        {
            _dataBase.Borrows.Add(borrow);
            _dataBase.SaveChanges();
        }

        public void AddCustomer(Customer customer)
        {
            _dataBase.Customers.Add(customer);
            _dataBase.SaveChanges();
        }

        public List<Book> GetBooks()
        {
            return _dataBase.Books.ToList();
        }

        public List<Borrow> GetBorrows()
        {
            return _dataBase.Borrows.ToList();
        }

        public List<Customer> GetCustomers()
        {
            return _dataBase.Customers.ToList();
        }

        public void RemoveBook(Book book)
        {
            _dataBase.Books.Remove(book);
            _dataBase.SaveChanges();
        }

        public void RemoveCustomer(Customer customer)
        {
            _dataBase.Customers.Remove(customer);
            _dataBase.SaveChanges();
        }

        public void Return(Borrow borrow)
        {
            _dataBase.Entry(borrow).State = System.Data.Entity.EntityState.Modified;
            _dataBase.SaveChanges();
        }

        public void ModifyBook(Book book)
        {
            _dataBase.Entry(book).State = System.Data.Entity.EntityState.Modified;
            _dataBase.SaveChanges();
        }
    }
}
