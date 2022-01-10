using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.Entity;

namespace LibraryService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class LibraryAPI : ILibraryAPI
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

        public string AddBook(Book newBook)
        {
            if (BookIsNullable(newBook))
            {
                return "Nie dodano książki, ponieważ conajmniej jedno z atrybutów nie zawiera wartości.";
            }

            using (LibraryDb db = new LibraryDb())
            {
                var books = db.Books.ToList();
                foreach (var book in books)
                {
                    if (IsSimilarBook(book, newBook))
                    {
                        book.Availability += newBook.Availability;

                        db.Entry(book).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();

                        return "Książka znajduje się w bazie biblioteki. Uzupełniono jej dostępność.";
                    }
                }

                db.Books.Add(newBook);
                db.SaveChanges();
            }

            return "Dodano Książkę do bazy danych.";
        }

        private bool IsSimilarBook(Book existing, Book created)
        {
            return existing.Title.Contains(created.Title) && existing.AuthorName.Contains(created.AuthorName)
                && existing.AuthorSurname.Contains(created.AuthorSurname);
        }

        private bool BookIsNullable(Book book)
        {
            return book.AuthorName == null || book.AuthorSurname == null || book.Description == null || book.Title == null || book.Price <= 0;
        }

        public string DeleteBook(int id)
        {
            Book book = new Book();

            using (LibraryDb db = new LibraryDb())
            {
                List<Book> books = db.Books.Where(x => x.Id == id).ToList();
                var q = db.Borrows.ToList();

                if (books.Count() == 0)
                {
                    return "Nie znaleziono wskazanej Książki w bazie biblioteki.";
                }
                else if (q.Where(x => x.Book.Id == id && x.Return == null).Count() != 0)
                {
                    return "Nie usunieto książki, ponieważ nie wszystkie egzemplarze zostały zwrócone";
                }

                book = books[0];

                db.Books.Remove(book);
                db.SaveChanges();
            }

            return $"Usunięto książkę, Tytuł: {book.Title} Autor: {book.AuthorName} {book.AuthorSurname}.";
        }

        public List<Book> GetBooks()
        {

            using (LibraryDb db = new LibraryDb())
            {
                return db.Books.ToList();
            }
        }

        public string Borrow(int customerId, int bookId)
        {
            Customer customer = new Customer();
            Book book = new Book();

            using (LibraryDb db = new LibraryDb())
            {
                var customersQuery = db.Customers.Where(x => x.Id == customerId).ToList();
                var booksQuery = db.Books.Where(x => x.Id == bookId).ToList();

                if (customersQuery.Count() == 0)
                {
                    return "Nie znaleziono wskazanego Klienta w bazie biblioteki.";
                }
                else if (booksQuery.Count() == 0)
                {
                    return "Nie znaleziono wskazanej Książki w bazie biblioteki.";
                }
                else if (booksQuery[0].Availability == 0)
                {
                    return "Wpożyczenie jest niemożliwe, ponieważ książki niema na stanie biblioteki.";
                }

                customer = customersQuery[0];
                book = booksQuery[0];
                Borrow borrow = new Borrow(customer, book);
                db.Borrows.Add(borrow);
                db.SaveChanges();
            }

            return $"Wyporzyczono, Tytuł: {book.Title}, Klientowi: {customer.Name} {customer.Surname}";
        }

        public string Return(int id)
        {
            Borrow borrow = new Borrow();
            using (LibraryDb db = new LibraryDb())
            {
                var borrowsQuery = db.Borrows.Where(x => x.Id == id).ToList();

                if (borrowsQuery.Count() == 0)
                {
                    return "Nie znaleziono wskazanego wypożyczenia w bazie biblioteki.";
                }

                borrow = borrowsQuery[0];
            }

            return $"Zwrócono, Tytuł: {borrow.Book.Title} Klienta: {borrow.Customer.Name} {borrow.Customer.Surname}";
        }

        public List<Borrow> GetBorrows()
        {
            using (LibraryDb db = new LibraryDb())
            {
                return db.Borrows.ToList();
            }
        }

    }
}
