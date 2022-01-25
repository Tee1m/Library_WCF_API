using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace LibraryService
{
    public class CustomersService : ICustomersService
    {
        private readonly ICustomersRepository _customersRepository;
        private readonly IBorrowsRepository _borrowsRepository;

        public CustomersService(ICustomersRepository customers, IBorrowsRepository borrows)
        {
            this._customersRepository = customers;
            this._borrowsRepository = borrows;
        }

        public string AddCustomer(Customer newCustomer)
        {
            if (CustomerIsNullable(newCustomer))
            {
                return "Nie dodano Klienta, ponieważ conajmniej jedno z atrybutów nie zawiera wartości.";
            }

            var customers = _customersRepository.Get();

            foreach (var existingCustomer in customers)
            {
                if (IsSimilarCustomer(existingCustomer, newCustomer))
                {
                    return "Nie dodano Klienta, ponieważ istnieje on w bazie biblioteki.";
                }
            }

            _customersRepository.Add(newCustomer);

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
            var customerQuery = _customersRepository.Get();
            var borrowsQuery = _borrowsRepository.Get();

            if (!customerQuery.Where(x => x.Id == id).Any())
            {
                return "Nie znaleziono wskazanego Klienta w bazie biblioteki.";
            }
            else if (borrowsQuery.Where(x => x.CustomerId == id).Any())
            {
                return "Nie usunieto Klienta, ponieważ posiada on jeszcze wypożyczone książki.";
            }

            var customer = customerQuery.Where(x => x.Id == id).Single();

            _customersRepository.Remove(customer);

            return $"Usunięto Klienta, P. {customer.Name} {customer.Surname}.";
        }

        public List<Customer> GetCustomers()
        {
            return _customersRepository.Get().ToList();
        }
    }
}
