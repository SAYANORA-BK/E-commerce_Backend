using System.ComponentModel.DataAnnotations;

namespace E_commerce.Dto
{
    public class OrderViewDto
    {
        [Required]
        public string TransactionId { get; set; }
        [Required]
        public decimal? TotalAmount { get; set; }
        [Required]
        public string DeliveryAdrress { get;internal set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public List<OrderItemDto> Items { get; set; }

    }
}

