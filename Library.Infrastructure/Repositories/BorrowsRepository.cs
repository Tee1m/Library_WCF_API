using System.Collections.Generic;
using System.Linq;
using Application;
using AutoMapper;
using System.Data.Entity;
using Domain;

namespace DAL
{
    public class BorrowsRepository : IBorrowsRepository
    {
        private readonly LibraryDb _context;
        private readonly IMapper _mapper;

        public BorrowsRepository(LibraryDb context, IMapper borrowsMapper)
        {
            this._context = context;
            this._mapper = borrowsMapper;
        }

        public void Add(Customer customerDTO, Book bookDTO)
        {
            var customer = _context.Customers.Single(a => a.Id == customerDTO.Id);
            var book = _context.Books.Single(a => a.Id == bookDTO.Id);

            var borrow = new BorrowDAL(customer, book);

            _context.Borrows.Add(borrow);
        }

        public void Update(Borrow obj)
        {
            var borrow = _context.Borrows.Single(a => a.Id == obj.Id);
            var translatedBorrow = _mapper.Map<BorrowDAL>(obj);

            borrow.CustomerId = translatedBorrow.CustomerId;
            borrow.BookId = translatedBorrow.BookId;
            borrow.DateOfBorrow = translatedBorrow.DateOfBorrow;
            borrow.Return = translatedBorrow.Return;

            _context.Entry(borrow).State = EntityState.Modified;
        }

        public void Remove(Borrow obj)
        {
            _context.Borrows.Remove(_context.Borrows.Single(a => a.Id == obj.Id));
        }

        public List<Borrow> Get()
        {
            var borrowsList = _context.Borrows.ToList();
            var borrowsDTOList = new List<Borrow>();

            foreach (var borrow in borrowsList)
            {
                borrowsDTOList.Add(_mapper.Map<Borrow>(borrow));
            }

            return borrowsDTOList;
        }
    }
}
