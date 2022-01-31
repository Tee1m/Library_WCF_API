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
        private Mapper _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile())));

        public ICustomersRepository CustomersRepository
        {
            get
            {
                if (CustomersRepository == null)
                    return new CustomersRepository(_context, _mapper);

                return CustomersRepository;
            }
        }

        public IBooksRepository BooksRepository
        {
            get
            {
                if (BooksRepository == null)
                    return new BooksRepository(_context, _mapper);

                return BooksRepository;
            }
        }

        public IBorrowsRepository BorrowsRepository
        {
            get
            {
                if (BorrowsRepository == null)
                    return new BorrowsRepository(_context, _mapper);

                return BorrowsRepository;
            }
        }

        public UnitOfWork(LibraryDb context)
        {
            this._context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
