using Domain;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace Application
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class CustomersService : ICustomersService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerUniquenessChecker _uniquenessChecker;
        private IBusinessRule _rule;

        public CustomersService(IUnitOfWork unitOfWork, ICustomerUniquenessChecker uniquenessChecker)
        {
            this._unitOfWork = unitOfWork;
            this._uniquenessChecker = uniquenessChecker;
        }

        public string AddCustomer(Customer newCustomer)
        {
            _rule = newCustomer.HasAllValues();

            if (_rule.NotValid())
                return _rule.Message;

            _rule = newCustomer.IsUnique(_uniquenessChecker);

            if(_rule.NotValid())
                return _rule.Message;

            _unitOfWork.CustomersRepository.Add(newCustomer);
            _unitOfWork.Commit();

            return $"Dodano Klienta, P. {newCustomer.Name} {newCustomer.Surname}";
        }

        public string DeleteCustomer(int id)
        {
            var customerQuery = _unitOfWork.CustomersRepository.Get();
            var borrowsQuery = _unitOfWork.BorrowsRepository.Get();

            if (!customerQuery.Where(x => x.Id == id).Any())
            {
                return "Nie znaleziono wskazanego Klienta w bazie biblioteki.";
            }
            else if (borrowsQuery.Where(x => x.CustomerId == id).Any())
            {
                return "Nie usunieto Klienta, ponieważ posiada on jeszcze wypożyczone książki.";
            }

            var customer = customerQuery.Where(x => x.Id == id).Single();

            _unitOfWork.CustomersRepository.Remove(customer);
            _unitOfWork.Commit();

            return $"Usunięto Klienta, P. {customer.Name} {customer.Surname}.";
        }

        public List<Customer> GetCustomers()
        {
            return _unitOfWork.CustomersRepository.Get().ToList();
        }
    }
}
