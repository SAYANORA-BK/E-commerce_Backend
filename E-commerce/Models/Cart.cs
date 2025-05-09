using System.ComponentModel.DataAnnotations;

namespace E_commerce.Models
{
    public class Cart
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        public User? User { get; set; }
        public virtual ICollection<CartItems>? cartitems { get; set; }
    }
}
