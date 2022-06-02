using System.Collections.Generic;
using System.Linq;
using Application;
using AutoMapper;
using System.Data.Entity;
using Domain;

namespace Infrastructure
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

        public void Add(Customer obj)
        {
            var customer = _mapper.Map<CustomerDAL>(obj);

            _context.Customers.Add(customer);
        }

        public void Update(Customer obj)
        {
            var customer = _context.Customers.Single(a => a.Id == obj.Id);
            var translatedCustomer = _mapper.Map<CustomerDAL>(obj);

            customer.Id = translatedCustomer.Id;
            customer.Name = translatedCustomer.Name;
            customer.Surname = translatedCustomer.Surname;
            customer.TelephoneNumber = translatedCustomer.TelephoneNumber;
            customer.Address = translatedCustomer.Address;

            _context.Entry(customer).State = EntityState.Modified;
        }

        public void Remove(Customer obj)
        {
            _context.Customers.Remove(_context.Customers.Single(a => a.Id == obj.Id));
        }

        public List<Customer> Get()
        {
            var customersList = _context.Customers.ToList();
            var customersDTOList = new List<Customer>();

            foreach (var customer in customersList)
            {
                customersDTOList.Add(_mapper.Map<Customer>(customer));
            }

            return customersDTOList;
        }

    }
}
