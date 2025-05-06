
using E_commerce.Dto;
using E_commerce.Models;

public interface IUserService
{
    Task<bool> Register(RegisterDto dto);
    Task<ResultDto> Login(LoginDto userdto);
}
