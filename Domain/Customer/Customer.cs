using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [DataContract]
    public class Customer
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Surname { get; set; }

        [DataMember]
        public string Address { get; set; }

        [DataMember]
        public string TelephoneNumber { get; set; }

        public List<Borrow> _borrows { get; private set; }

        public Customer() 
        {
            _borrows = new List<Borrow>();
        }

        public IBusinessRule HasAllValues() => new CustomerHasAllValues(this);

        public IBusinessRule IsUnique(ICustomerUniquenessChecker checker)
        {
            return new CustomerTelephoneNumberUniqueRule(checker, TelephoneNumber);
        }

        
    }
}


