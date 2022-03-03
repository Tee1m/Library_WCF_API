using AutoMapper;
using LibraryService;

namespace Library.Infrastructure
{
    public class BookMappingProfile : Profile
    {
        public BookMappingProfile()
        {
            CreateMap<Book, BookDTO>().ReverseMap();
        }
    }
}
