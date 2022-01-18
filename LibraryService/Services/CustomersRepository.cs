using System.Collections.Generic;
using System.Linq;

namespace LibraryService
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly IDatabaseClient _dbClient;

        public CustomersRepository(IDatabaseClient dbClient)
        {
            this._dbClient = dbClient;
        }

        public string AddCustomer(Customer newCustomer)
        {
            if (CustomerIsNullable(newCustomer))
            {
                return "Nie dodano Klienta, ponieważ conajmniej jedno z atrybutów nie zawiera wartości.";
            }

            var customers = _dbClient.GetCustomers();

            foreach (var existingCustomer in customers)
            {
                if (IsSimilarCustomer(existingCustomer, newCustomer))
                {
                    return "Nie dodano Klienta, ponieważ istnieje on w bazie biblioteki.";
                }
            }

            _dbClient.AddCustomer(newCustomer);

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
            Customer customer = new Customer();

            var customerQuery = _dbClient.GetCustomers().Where(x => x.Id == id).ToList();
            var borrowsQuery = _dbClient.GetBorrows().ToList();

            if (customerQuery.Count() == 0)
            {
                return "Nie znaleziono wskazanego Klienta w bazie biblioteki.";
            }
            else if (borrowsQuery.Where(x => x.Customer.Id == id).Count() != 0)
            {
                return "Nie usunieto Klienta, ponieważ posiada on jeszcze wypożyczone książki.";
            }
            customer = customerQuery[0];

            _dbClient.RemoveCustomer(customer);

            return $"Usunięto Klienta, P. {customer.Name} {customer.Surname}.";
        }

        public List<Customer> GetCustomers()
        {
            return _dbClient.GetCustomers();
        }
    }
}
