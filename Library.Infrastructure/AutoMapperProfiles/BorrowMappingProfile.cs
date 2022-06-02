using AutoMapper;
using Domain;
using Application;

namespace Infrastructure
{
    public class BorrowMappingProfile : Profile
    {
        public BorrowMappingProfile()
        {
            CreateMap<Borrow, BorrowDAL>()
                .ForMember(dest => dest.Id, src => src.MapFrom(value => value.Id))
                .ForMember(dest => dest.DateOfBorrow, src => src.MapFrom(value => value.DateOfBorrow))
                .ForMember(dest => dest.BookId, src => src.MapFrom(value => value.BookId))
                .ForMember(dest => dest.Return, src => src.MapFrom(value => value.Return))
                .ForMember(dest => dest.CustomerId, src => src.MapFrom(value => value.CustomerId))
                .ForMember(dest => dest.CustomerDAL, src => src.Ignore())
                .ForMember(dest => dest.BookDAL, src => src.Ignore());

            CreateMap<BorrowDAL, Borrow>()
                .ForMember(dest => dest.Id, src => src.MapFrom(value => value.Id))
                .ForMember(dest => dest.DateOfBorrow, src => src.MapFrom(value => value.DateOfBorrow))
                .ForMember(dest => dest.CustomerId, src => src.MapFrom(value => value.CustomerId))
                .ForMember(dest => dest.BookId, src => src.MapFrom(value => value.BookId))
                .ForMember(dest => dest.Return, src => src.MapFrom(value => value.Return))
                .ForMember(dest => dest.Customer, src => src.MapFrom(value => value.CustomerDAL.ToString()))
                .ForMember(dest => dest.Book, src => src.MapFrom(value => value.BookDAL.ToString()));

            CreateMap<string, BookDAL>(MemberList.None);

            CreateMap<string, CustomerDAL>(MemberList.None);
        }
    }
}
