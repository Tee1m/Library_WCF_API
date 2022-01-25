using System.Collections.Generic;
using System.Linq;

namespace LibraryService
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly IUnitOfWork _context;

        public CustomersRepository(IUnitOfWork context)
        {
            this._context = context;
        }

        public void Add(Customer obj)
        {
            _context.Add<Customer>(obj);
            _context.Commit();
        }

        public void Attach(Customer obj)
        {
            _context.Attach<Customer>(obj);
        }

        public List<Customer> Get()
        {
            return _context.Get<Customer>().ToList<Customer>();
        }

        public void Remove(Customer obj)
        {
            _context.Remove<Customer>(obj);
            _context.Commit();
        }
    }
}
