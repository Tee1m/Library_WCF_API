using System.Collections.Generic;
using System.Linq;

namespace LibraryService
{
    public class BorrowsRepository : IBorrowsRepository
    {
        private readonly IUnitOfWork _context;

        public BorrowsRepository(IUnitOfWork context)
        {
            this._context = context;
        }

        public void Add(Borrow obj)
        {
            _context.Add<Borrow>(obj);
            _context.Commit();
        }

        public void Attach(Borrow obj)
        {
            _context.Attach<Borrow>(obj);
        }

        public List<Borrow> Get()
        {
            return _context.Get<Borrow>().ToList<Borrow>();
        }

        public void Remove(Borrow obj)
        {
            _context.Remove<Borrow>(obj);
            _context.Commit();
        }
    }
}
