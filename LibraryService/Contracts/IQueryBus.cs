using Domain;
using System.Collections.Generic;
using System.ServiceModel;

namespace Application
{
    [ServiceContract]
    public interface IBooksService
    {
        [OperationContract]
        string AddBook(Book newBook);

        [OperationContract]
        string DeleteBook(int id);

        [OperationContract]
        List<Book> GetBooks();
    }

}
