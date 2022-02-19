using System.Collections.Generic;
using System.Linq;
using LibraryService;
using AutoMapper;
using System.Data.Entity;

namespace Library.Infrastructure
{
    public class BooksRepository : IBooksRepository
    {
        private readonly LibraryDb _context;
        private readonly IMapper _mapper;

        public BooksRepository(LibraryDb context, IMapper bookMapper)
        {
            this._context = context;
            this._mapper = bookMapper;
        }

        public void Add(BookDTO obj)
        {
            var book = _mapper.Map<Book>(obj);

            _context.Books.Add(book);
        }

        public void Update(BookDTO obj)
        { 
            var book = _context.Books.Single(a => a.Id == obj.Id);
            var translatedBook = _mapper.Map<Book>(obj);

            book.AuthorName = translatedBook.AuthorName;
            book.AuthorSurname = translatedBook.AuthorSurname;
            book.Amount = translatedBook.Amount;
            book.Title = translatedBook.Title;
            book.Description = translatedBook.Description;

            _context.Entry(book).State = EntityState.Modified;
        }

        public void Remove(BookDTO obj)
        {
            var book = _mapper.Map<Book>(obj);

            _context.Books.Remove(_context.Books.Single(a => a.Id == obj.Id));
        }

        public List<BookDTO> Get()
        {
            var booksList = _context.Books.ToList();
            var booksDTOList = new List<BookDTO>();

            foreach (var book in booksList)
            {
                booksDTOList.Add(_mapper.Map<BookDTO>(book));
            }

            return booksDTOList;
        }
    }
}
