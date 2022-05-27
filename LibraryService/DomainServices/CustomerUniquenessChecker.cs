using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Application
{
    public class CustomerUniquenessChecker : ICustomerUniquenessChecker
    {
        ICustomersRepository _customersRepository;

        public CustomerUniquenessChecker(ICustomersRepository customersRepository)
        {
            this._customersRepository = customersRepository;
        }

        public bool IsUnique(string telephoneNumber)
        {
            return _customersRepository.Get().Where(x => x.TelephoneNumber == telephoneNumber).Any();
        }
    }
}
