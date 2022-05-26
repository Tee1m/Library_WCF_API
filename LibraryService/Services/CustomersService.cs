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

        public CustomersService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public string AddCustomer(Customer newCustomer)
        {
            if (CustomerIsNullable(newCustomer))
            {
                return "Nie dodano Klienta, ponieważ conajmniej jedno z atrybutów nie zawiera wartości.";
            }

            var customers = _unitOfWork.CustomersRepository.Get();

            foreach (var existingCustomer in customers)
            {
                if (IsSimilarCustomer(existingCustomer, newCustomer))
                {
                    return "Nie dodano Klienta, ponieważ istnieje on w bazie biblioteki.";
                }
            }

            _unitOfWork.CustomersRepository.Add(newCustomer);
            _unitOfWork.Commit();

            return $"Dodano Klienta, P. {newCustomer.Name} {newCustomer.Surname}";
        }

        private bool CustomerIsNullable(Customer customer)
        {
            return customer.Name == null || customer.Surname == null || customer.Address == null || customer.TelephoneNumber == "";
        }

        private bool IsSimilarCustomer(Customer existing, Customer created)
        {
            return existing.Name.Contains(created.Name) && existing.Surname.Contains(created.Surname) &&
                   existing.Address.Contains(created.Address) && existing.TelephoneNumber == created.TelephoneNumber;
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
