using System.ComponentModel.DataAnnotations;

namespace E_commerce.Dto
{
    public class WishListDto
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int ProductId { get; set; }
    }
}
