using AutoMapper;
using Domain;
using Application;

namespace DAL
{
    public class BookMappingProfile : Profile
    {
        public BookMappingProfile()
        {
            CreateMap<BookDAL, Book>().ReverseMap();
        }
    }
}
