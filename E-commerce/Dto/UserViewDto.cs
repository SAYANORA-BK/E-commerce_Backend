using E_commerce.Dto;
using E_commerce.Models;

namespace E_commerce.Dto
{
    public class UserViewDto
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
        public virtual List<OrderViewDto>? Orders { get; set; }
        public List<AddressDto> Address { get; set; }
    }
}


