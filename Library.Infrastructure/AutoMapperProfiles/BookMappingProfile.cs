using AutoMapper;
using Domain;
using Application;

namespace Infrastructure
{
    public class BookMappingProfile : Profile
    {
        public BookMappingProfile()
        {
            CreateMap<BookDAL, Book>().ReverseMap();
        }
    }
}
