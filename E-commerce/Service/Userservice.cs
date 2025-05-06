using AutoMapper;
using E_commerce.Dbcontext;
using E_commerce.Dto;
using E_commerce.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ECommerceAPI.Services
{
    public class Userservice : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public Userservice(AppDbContext context,IMapper mapper,IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<bool> Register(RegisterDto dto)
        {
            var res = await _context.users.FirstOrDefaultAsync(x=>x.Email==dto.Email);
            if (res != null)
            {
                return false;
            }
            dto.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            var user=_mapper.Map<User>(dto);
            _context.users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ResultDto> Login(LoginDto userdto)
        {
            try
            {
              

                var usr = await _context.users.FirstOrDefaultAsync(u => u.Email == userdto.Email);
                if (usr == null)
                {
                   
                    return new ResultDto { Error = "Not Found" };
                }
                if (usr.IsBlocked == true)
                {
               
                    return new ResultDto { Error = "user is blocked" };
                }

                var pass = ValidatePassword(userdto.Password, usr.Password);

                if (!pass)
                {
                    return new ResultDto { Error = "Invalid Password" };
                }

               
                var token = GenerateToken(usr);
                return new ResultDto
                {
                    Token = token,
                    Role = usr.Role,
                    Email = usr.Email,
                    Id = usr.Id,
                    Name = usr.FirstName

                };
            }
            catch (Exception ex)
            {
               
                throw;
            }
        }
        public string GenerateToken(User user)
        {

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.Email),
        new Claim(ClaimTypes.Role, user.Role)
    };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = "YourIssuer",
                Audience = "YourAudience",
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        private bool ValidatePassword(string password, string hashPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashPassword);
        }

    }
}