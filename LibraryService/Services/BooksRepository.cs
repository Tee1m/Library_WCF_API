using System.Collections.Generic;
using System.Linq;

namespace LibraryService
{
    public class BooksRepository : IBooksRepository
    {
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
                db.SaveChanges(); //TO DO ENTIES EXCEPTION
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
            return book.AuthorName == null || book.AuthorSurname == null || book.Description == "" || book.Title == null;
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
                else if (q == null)
                {
                    if (q.Where(x => x.Book.Id == id && x.Return == null).Count() != 0)
                    {
                        return "Nie usunieto książki, ponieważ nie wszystkie egzemplarze zostały zwrócone";
                    }    
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
    }
}
