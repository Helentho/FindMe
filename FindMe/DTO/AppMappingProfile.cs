using AutoMapper;
using FindMe.Models;

namespace FindMe.DTO
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<UserDTO, User>().ReverseMap();
        }
    }
}
