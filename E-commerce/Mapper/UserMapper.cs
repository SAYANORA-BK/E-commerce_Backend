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
            CreateMap<Category, CategoryViewDto>().ReverseMap();
            CreateMap<AddProductDto, Product>().ReverseMap();
            CreateMap<WishList, WishListDto>().ReverseMap();
            CreateMap<Order, OrderViewDto>().ReverseMap();
            CreateMap<Address,CreateAddressDto>().ReverseMap();
            CreateMap<Address,ShowAddressDto>().ReverseMap();
            CreateMap<User, UserViewDto>().ReverseMap();

        }
    }
}

