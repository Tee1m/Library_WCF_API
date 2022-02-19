using System.Collections.Generic;
using System.Linq;
using LibraryService;
using AutoMapper;
using System.Data.Entity;

namespace Library.Infrastructure
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly LibraryDb _context;
        private readonly IMapper _mapper; 

        public CustomersRepository(LibraryDb context, IMapper customerMapper)
        {
            this._context = context;
            this._mapper = customerMapper;
        }

        public void Add(CustomerDTO obj)
        {
            var customer = _mapper.Map<Customer>(obj);

            _context.Customers.Add(customer);
        }

        public void Update(CustomerDTO obj)
        {
            var customer = _context.Customers.Single(a => a.Id == obj.Id);
            var translatedCustomer = _mapper.Map<Customer>(obj);

            customer.Id = translatedCustomer.Id;
            customer.Name = translatedCustomer.Name;
            customer.Surname = translatedCustomer.Surname;
            customer.TelephoneNumber = translatedCustomer.TelephoneNumber;
            customer.Address = translatedCustomer.Address;

            _context.Entry(customer).State = EntityState.Modified;
        }

        public void Remove(CustomerDTO obj)
        {
            _context.Customers.Remove(_context.Customers.Single(a => a.Id == obj.Id));
        }

        public List<CustomerDTO> Get()
        {
            var customersList = _context.Customers.ToList();
            var customersDTOList = new List<CustomerDTO>();

            foreach (var customer in customersList)
            {
                customersDTOList.Add(_mapper.Map<CustomerDTO>(customer));
            }

            return customersDTOList;
        }

    }
}
