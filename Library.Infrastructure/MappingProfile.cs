using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LibraryService;

namespace Library.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BorrowDTO, Borrow>()
                .ForMember(a => a.Id, b => b.MapFrom(c => c.Id))
                .ForMember(a => a.DateOfBorrow, b => b.MapFrom(c => c.DateOfBorrow))
                .ForMember(a => a.CustomerId, b => b.MapFrom(c => c.CustomerId))
                .ForMember(a => a.BookId, b => b.MapFrom(c => c.BookId))
                .ForMember(a => a.Return, b => b.MapFrom(c => c.Return));

            CreateMap<Borrow, BorrowDTO>()
                .ForMember(a => a.Id, b => b.MapFrom(c => c.Id))
                .ForMember(a => a.DateOfBorrow, b => b.MapFrom(c => c.DateOfBorrow))
                .ForMember(a => a.CustomerId, b => b.MapFrom(c => c.CustomerId))
                .ForMember(a => a.BookId, b => b.MapFrom(c => c.BookId))
                .ForMember(a => a.Return, b => b.MapFrom(c => c.Return))
                .ForMember(a => a.Customer, b => b.MapFrom(c => c.Customer.ToString()))
                .ForMember(a => a.Book, b => b.MapFrom(c => c.Book.ToString()));

            CreateMap<Book, BookDTO>().ReverseMap();

            CreateMap<Customer, CustomerDTO>().ReverseMap();
        }
    }
}
