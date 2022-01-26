using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryService
{
    public class BookDTOBuilder
    {
        private readonly BookDTO _book = new BookDTO();

        public BookDTOBuilder SetId(int id)
        {
            _book.Id = id;
            return this;
        }

        public BookDTOBuilder SetTitle(string title)
        {
            _book.Title = title;
            return this;
        }

        public BookDTOBuilder SetAuthorName(string authorName)
        {
            _book.AuthorName = authorName;
            return this;
        }

        public BookDTOBuilder SetAuthorSurname(string authorSurname)
        {
            _book.AuthorSurname = authorSurname;
            return this;
        }

        public BookDTOBuilder SetAvailability(int availability)
        {
            _book.Amount = availability;
            return this;
        }

        public BookDTOBuilder SetDescription(string description)
        {
            _book.Description = description;
            return this;
        }

        public BookDTO Build()
        {
            return _book;
        }
    }
}
