using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace LibraryService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class LibraryAPI : ILibraryAPI
    {
        public bool AddCustomer(Customer newCustomer)
        {
            using (LibraryDb db = new LibraryDb())
            {
                var q = db.Customers.ToList();

                foreach (var existingCustomer in q)
                {
                    if(IsSimilarCustomer(existingCustomer, newCustomer) || CustomerIsNullable(newCustomer))
                    {
                        return false;
                    }
                }

                db.Customers.Add(newCustomer);
                db.SaveChanges();
            }

            return true;
        }

        public bool DeleteCustomer(Customer customer)
        {
            using (LibraryDb db = new LibraryDb())
            {
                var q = db.Customers.ToList();

                foreach (var existingCustomer in q)
                {
                    if (IsSimilarCustomer(existingCustomer, customer))
                    {
                        db.Customers.Remove(existingCustomer);
                        db.SaveChanges();

                        return true;
                    }
                }
            }

            return false;
        }

        bool IsSimilarCustomer(Customer existing, Customer created)
        {
            return existing.Name.Contains(created.Name) && existing.Surname.Contains(created.Surname) &&
                   existing.Address.Contains(created.Address) && existing.TelephoneNumber == created.TelephoneNumber;
        }

        bool CustomerIsNullable(Customer customer)
        {
            return customer.Name == null || customer.Surname == null || customer.Address == null || customer.TelephoneNumber == null;
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
