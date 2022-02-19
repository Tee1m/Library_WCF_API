﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryService;

namespace LibraryService
{
    public interface IUnitOfWork
    {
        IBooksRepository BooksRepository { get; }
        IBorrowsRepository BorrowsRepository { get; }
        ICustomersRepository CustomersRepository { get; }

        void Commit();
    }
}
