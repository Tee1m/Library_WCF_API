using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryService;
using AutoMapper;

namespace Library.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private LibraryDb _context;
        private IMapper _mapper;
        private IBooksRepository _booksRepository;
        private IBorrowsRepository _borrowsRepository;
        private ICustomersRepository _customersRepository;

        public IBooksRepository BooksRepository
        {
            get
            {
                if (_booksRepository == null)
                {
                    _booksRepository = new BooksRepository(_context, _mapper);
                }

                return _booksRepository;
            }

            private set { }
        }

        public IBorrowsRepository BorrowsRepository
        {
            get
            {
                if (_borrowsRepository == null)
                {
                    _borrowsRepository = new BorrowsRepository(_context, _mapper);
                }

                return _borrowsRepository;
            }

            private set { }
        }

        public ICustomersRepository CustomersRepository
        {
            get
            {
                if (_customersRepository == null)
                {
                    _customersRepository = new CustomersRepository(_context, _mapper);
                }

                return _customersRepository;
            }

            private set { }
        }

        public UnitOfWork(LibraryDb context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}
