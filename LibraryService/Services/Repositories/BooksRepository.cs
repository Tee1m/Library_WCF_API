using System.Collections.Generic;
using System.Linq;

namespace LibraryService
{
    public class BooksRepository : IBooksRepository
    {
        private readonly IUnitOfWork _context;

        public BooksRepository(IUnitOfWork context)
        {
            this._context = context;
        }

        public void Add(Book obj)
        {
            _context.Add<Book>(obj);
            _context.Commit();
        }

        public void Attach(Book obj)
        {
            _context.Attach<Book>(obj);
        }

        public List<Book> Get()
        {
            return _context.Get<Book>().ToList<Book>();
        }

        public void Remove(Book obj)
        {
            _context.Remove<Book>(obj);
            _context.Commit();
        }
    }
}
