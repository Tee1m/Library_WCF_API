using System.Collections.Generic;
using System.ServiceModel;

namespace LibraryService
{
    [ServiceContract]
    public interface IBooksRepository
    {
        [OperationContract]
        string AddBook(Book newBook);

        [OperationContract]
        string DeleteBook(int id);

        [OperationContract]
        List<Book> GetBooks();
    }

}
