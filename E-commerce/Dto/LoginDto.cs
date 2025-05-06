using System.ComponentModel.DataAnnotations;

namespace E_commerce.Dto
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }
        [Required]

        public string? Password { get; set; }
    }
}
