using System.Collections.Generic;
using System.ServiceModel;

namespace LibraryService
{
    [ServiceContract]
    public interface IBooksService
    {
        [OperationContract]
        string AddBook(BookDTO newBook);

        [OperationContract]
        string DeleteBook(int id);

        [OperationContract]
        List<BookDTO> GetBooks();
    }

}
