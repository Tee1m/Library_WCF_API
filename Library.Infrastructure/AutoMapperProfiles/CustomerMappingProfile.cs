using AutoMapper;
using Domain;
using Application;

namespace DAL
{
    public class CustomerMappingProfile : Profile
    {
        public CustomerMappingProfile()
        {
            CreateMap<CustomerDAL, Customer>()
                .ForMember(dest => dest._borrows, src => src.Ignore())
                .ReverseMap();
        }
    }
}
