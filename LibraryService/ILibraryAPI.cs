using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace LibraryService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface ILibraryAPI
    { 
        [OperationContract]
        string AddCustomer(Customer newCustomer);

        [OperationContract]
        string DeleteCustomer(int id);
        
        [OperationContract]
        List<Customer> GetCustomers();
        
        [OperationContract]
        string AddBook(Book newBook);
        
        [OperationContract]
        string DeleteBook(int id);

        [OperationContract]
        List<Book> GetBooks();

        [OperationContract]
        string Borrow(int customerId, int bookId);

        [OperationContract]
        string Return(int id);

        [OperationContract]
        List<Borrow> GetBorrows();

        // TODO: Add your service operations here
    }

}
