using System.Collections.Generic;
using System.Linq;
using LibraryService;
using AutoMapper;

namespace Library.Infrastructure
{
    public class BorrowsRepository : IBorrowsRepository
    {
        private readonly IUnitOfWork _context;
        private readonly IMapper _mapper;
        public BorrowsRepository(IUnitOfWork context, IMapper borrowsMapper)
        {
            this._context = context;
            this._mapper = borrowsMapper;
        }

        public void Add(CustomerDTO customerDTO, BookDTO bookDTO)
        {
            var customer = _mapper.Map<Customer>(customerDTO);
            var book = _mapper.Map<Book>(bookDTO);

            var borrow = new Borrow(customer, book);

            _context.Add<Borrow>(borrow);
            _context.Commit();
        }

        public void Attach(BorrowDTO obj)
        {
            var borrow = _mapper.Map<Borrow>(obj);

            _context.Attach<Borrow>(borrow);
            _context.Commit();
        }

        public List<BorrowDTO> Get()
        {
            var borrowsList = _context.Get<Borrow>().ToList<Borrow>();
            var borrowsDTOList = new List<BorrowDTO>();

            foreach (var borrow in borrowsList)
            {
                borrowsDTOList.Add(_mapper.Map<BorrowDTO>(borrow));
            }

            return borrowsDTOList;
        }

        public void Remove(BorrowDTO obj)
        {
            var borrow = _mapper.Map<Borrow>(obj);

            _context.Remove<Borrow>(borrow);
            _context.Commit();
        }
    }
}
