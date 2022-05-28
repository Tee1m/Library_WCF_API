using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string TelephoneNumber { get; set; }

        public List<Borrow> _borrows { get; private set; }

        public Customer() 
        {
            _borrows = new List<Borrow>();
        }

        public IBusinessRule HasAllValues() => new CustomerHasAllValues(this);

        public IBusinessRule IsUnique(ICustomerUniquenessChecker checker) => new CustomerTelephoneNumberUniqueRule(checker, TelephoneNumber);        
    }
}


