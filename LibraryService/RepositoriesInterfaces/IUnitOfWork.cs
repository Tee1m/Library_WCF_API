using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryService
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomersRepository CustomersRepository { get; }
        IBooksRepository BooksRepository { get; }
        IBorrowsRepository BorrowsRepository { get; }
        void Commit();

    }
}
