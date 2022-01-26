using System.Collections.Generic;
using System.Linq;
using LibraryService;
using AutoMapper;

namespace Library.Infrastructure
{
    public class BooksRepository : IBooksRepository
    {
        private readonly IUnitOfWork _context;
        private readonly IMapper _mapper;

        public BooksRepository(IUnitOfWork context, IMapper bookMapper)
        {
            this._context = context;
            this._mapper = bookMapper;
        }

        public void Add(BookDTO obj)
        {
            var book = _mapper.Map<Book>(obj);

            _context.Add<Book>(book);
            _context.Commit();
        }

        public void Attach(BookDTO obj)
        {
            var book = _mapper.Map<Book>(obj);

            _context.Attach<Book>(book);
            _context.Commit();
        }

        public List<BookDTO> Get()
        {
            var booksList = _context.Get<Book>().ToList<Book>();
            var booksDTOList = new List<BookDTO>();

            return booksDTOList;
        }

        public void Remove(BookDTO obj)
        {
            var book = _mapper.Map<Book>(obj);

            _context.Remove<Book>(book);
            _context.Commit();
        }
    }
}
