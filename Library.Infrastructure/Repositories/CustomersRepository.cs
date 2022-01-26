using System.Collections.Generic;
using System.Linq;
using LibraryService;
using AutoMapper;

namespace Library.Infrastructure
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly IUnitOfWork _context;
        private readonly IMapper _mapper; 

        public CustomersRepository(IUnitOfWork context, IMapper customerMapper)
        {
            this._context = context;
            this._mapper = customerMapper;
        }

        public void Add(CustomerDTO obj)
        {
            var customer = _mapper.Map<Customer>(obj);

            _context.Add<Customer>(customer);
            _context.Commit();
        }

        public void Attach(CustomerDTO obj)
        {
            var customer = _mapper.Map<Customer>(obj);

            _context.Attach<Customer>(customer);
            _context.Commit();
        }

        public void Remove(CustomerDTO obj)
        {
            var customer = _mapper.Map<Customer>(obj);

            _context.Remove<Customer>(customer);
            _context.Commit();
        }

        public List<CustomerDTO> Get()
        {
            var customersList = _context.Get<Customer>().ToList<Customer>();
            var customersDTOList = new List<CustomerDTO>();

            foreach (var customer in customersList)
            {
                customersDTOList.Add(_mapper.Map<CustomerDTO>(customer));
            }

            return customersDTOList;
        }

        
    }
}
