using AutoMapper;
using LibraryService;

namespace Library.Infrastructure
{
    public class BorrowMappingProfile : Profile
    {
        public BorrowMappingProfile()
        {
            CreateMap<BorrowDTO, Borrow>()
                .ForMember(dest => dest.Id, src => src.MapFrom(value => value.Id))
                .ForMember(dest => dest.DateOfBorrow, src => src.MapFrom(value => value.DateOfBorrow))
                .ForMember(dest => dest.BookId, src => src.MapFrom(value => value.BookId))
                .ForMember(dest => dest.Return, src => src.MapFrom(value => value.Return))
                .ForMember(dest => dest.CustomerId, src => src.MapFrom(value => value.CustomerId))
                .ForMember(dest => dest.Customer, src => src.Ignore())
                .ForMember(dest => dest.Book, src => src.Ignore());

            CreateMap<Borrow, BorrowDTO>()
                .ForMember(dest => dest.Id, src => src.MapFrom(value => value.Id))
                .ForMember(dest => dest.DateOfBorrow, src => src.MapFrom(value => value.DateOfBorrow))
                .ForMember(dest => dest.CustomerId, src => src.MapFrom(value => value.CustomerId))
                .ForMember(dest => dest.BookId, src => src.MapFrom(value => value.BookId))
                .ForMember(dest => dest.Return, src => src.MapFrom(value => value.Return))
                .ForMember(dest => dest.Customer, src => src.MapFrom(value => value.Customer.ToString()))
                .ForMember(dest => dest.Book, src => src.MapFrom(value => value.Book.ToString()));

            CreateMap<string, Book>(MemberList.None);

            CreateMap<string, Customer>(MemberList.None);
        }
    }
}
