using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryService
{
    public class BookBuilder
    {
        private readonly Book _book = new Book();

        public BookBuilder SetId(int id)
        {
            _book.Id = id;
            return this;
        }

        public BookBuilder SetTitle(string title)
        {
            _book.Title = title;
            return this;
        }

        public BookBuilder SetAuthorName(string authorName)
        {
            _book.AuthorName = authorName;
            return this;
        }

        public BookBuilder SetAuthorSurname(string authorSurname)
        {
            _book.AuthorSurname = authorSurname;
            return this;
        }

        public BookBuilder SetAvailability(int availability)
        {
            _book.Availability = availability;
            return this;
        }

        public BookBuilder SetDescription(string description)
        {
            _book.Description = description;
            return this;
        }

        public Book Build()
        {
            return _book;
        }
    }
}
