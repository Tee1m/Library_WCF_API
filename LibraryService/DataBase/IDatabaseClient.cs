using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryService
{
    public interface IDatabaseClient
    {
        void AddCustomer(Customer customer);
        void AddBook(Book book);
        void AddBorrow(Borrow borrow);
        List<Customer> GetCustomers();
        List<Book> GetBooks();
        List<Borrow> GetBorrows();
        void RemoveCustomer(Customer customer);
        void RemoveBook(Book book);
        void Return(Borrow borrow);
        void ModifyBook(Book book);
    }
}
