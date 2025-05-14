using E_commerce.Models;

namespace E_commerce.Dto
{
    public class UserViewDto
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
        public virtual List<Order>? Orders { get; set; }
        public virtual Cart? Cart { get; set; }
    }
}
