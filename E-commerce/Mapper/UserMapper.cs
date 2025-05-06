using AutoMapper;
using E_commerce.Models;
using E_commerce.Dto;

namespace E_commerce.Mapper
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<User, RegisterDto>().ReverseMap();
            CreateMap<User, LoginDto>().ReverseMap();
        }
    }
}

