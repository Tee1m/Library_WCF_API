using System.Collections.Generic;
using System.Linq;
using Application;
using AutoMapper;
using System.Data.Entity;
using Domain;

namespace DAL
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

        public void Add(Book obj)
        {
            var book = _mapper.Map<BookDAL>(obj);

            _context.Books.Add(book);
        }

        public void Update(Book obj)
        { 
            var book = _context.Books.Single(a => a.Id == obj.Id);
            var translatedBook = _mapper.Map<BookDAL>(obj);

            book.AuthorName = translatedBook.AuthorName;
            book.AuthorSurname = translatedBook.AuthorSurname;
            book.Amount = translatedBook.Amount;
            book.Title = translatedBook.Title;
            book.Description = translatedBook.Description;

            _context.Entry(book).State = EntityState.Modified;
        }

        public void Remove(Book obj)
        {
            var book = _mapper.Map<BookDAL>(obj);

            _context.Books.Remove(_context.Books.Single(a => a.Id == obj.Id));
        }

        public List<Book> Get()
        {
            var booksList = _context.Books.ToList();
            var booksDTOList = new List<Book>();

            foreach (var book in booksList)
            {
                booksDTOList.Add(_mapper.Map<Book>(book));
            }

            return booksDTOList;
        }
    }
}
