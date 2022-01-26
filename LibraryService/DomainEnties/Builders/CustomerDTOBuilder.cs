using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryService
{
    public class CustomerDTOBuilder
    {
        private readonly CustomerDTO _customer = new CustomerDTO();

        public CustomerDTOBuilder SetId(int id)
        {
            _customer.Id = id;
            return this;
        }

        public CustomerDTOBuilder SetName(string name)
        {
            _customer.Name = name;
            return this;
        }

        public CustomerDTOBuilder SetSurname(string surname)
        {
            _customer.Surname = surname;
            return this;
        }

        public CustomerDTOBuilder SetAddress(string address)
        {
            _customer.Address = address;
            return this;
        }

        public CustomerDTOBuilder SetTelephoneNumber(string telephoneNumber)
        {
            _customer.TelephoneNumber = telephoneNumber;
            return this;
        }

        public CustomerDTO Build()
        {
            return _customer;
        }
    }
}
