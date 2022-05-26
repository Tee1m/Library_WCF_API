using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Common
{
    public class CustomerBuilder
    {
        private readonly Customer _customer = new Customer();

        public CustomerBuilder SetId(int id)
        {
            _customer.Id = id;
            return this;
        }

        public CustomerBuilder SetName(string name)
        {
            _customer.Name = name;
            return this;
        }

        public CustomerBuilder SetSurname(string surname)
        {
            _customer.Surname = surname;
            return this;
        }

        public CustomerBuilder SetAddress(string address)
        {
            _customer.Address = address;
            return this;
        }

        public CustomerBuilder SetTelephoneNumber(string telephoneNumber)
        {
            _customer.TelephoneNumber = telephoneNumber;
            return this;
        }

        public Customer Build()
        {
            return _customer;
        }
    }
}
