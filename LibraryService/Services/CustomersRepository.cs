using System.Collections.Generic;
using System.Linq;

namespace LibraryService
{
    public class CustomersRepository : ICustomersRepository
    {
        public string AddCustomer(Customer newCustomer)
        {
            if (CustomerIsNullable(newCustomer))
            {
                return "Nie dodano Klienta, ponieważ conajmniej jedno z atrybutów nie zawiera wartości.";
            }

            using (LibraryDb db = new LibraryDb())
            {
                var customers = db.Customers.ToList();

                foreach (var existingCustomer in customers)
                {
                    if (IsSimilarCustomer(existingCustomer, newCustomer))
                    {
                        return "Nie dodano Klienta, ponieważ istnieje on w bazie biblioteki.";
                    }
                }

                db.Customers.Add(newCustomer);
                db.SaveChanges();
            }

            return $"Dodano Klienta, P. {newCustomer.Name} {newCustomer.Surname}";
        }

        private bool CustomerIsNullable(Customer customer)
        {
            return customer.Name == null || customer.Surname == null || customer.Address == null || customer.TelephoneNumber == 0;
        }

        private bool IsSimilarCustomer(Customer existing, Customer created)
        {
            return existing.Name.Contains(created.Name) && existing.Surname.Contains(created.Surname) &&
                   existing.Address.Contains(created.Address) && existing.TelephoneNumber == created.TelephoneNumber;
        }

        public string DeleteCustomer(int id)
        {
            Customer customer = new Customer();

            using (LibraryDb db = new LibraryDb())
            {
                var customerQuery = db.Customers.Where(x => x.Id == id).ToList();
                var borrowsQuery = db.Borrows.ToList();

                if (customerQuery.Count() == 0)
                {
                    return "Nie znaleziono wskazanego Klienta w bazie biblioteki.";
                }
                else if (borrowsQuery.Where(x => x.Customer.Id == id).Count() != 0)
                {
                    return "Nie usunieto Klienta, ponieważ posiada on jeszcze wypożyczone książki.";
                }
                customer = customerQuery[0];

                db.Customers.Remove(customer);
                db.SaveChanges();
            }

            return $"Usunięto Klienta, P. {customer.Name} {customer.Surname}.";
        }

        public List<Customer> GetCustomers()
        {
            using (LibraryDb db = new LibraryDb())
            {
                return db.Customers.ToList();
            }
        }
    }
}
