using E_commerce.Dto;
using E_commerce.Models;

namespace E_commerce.Dto
{
    public class AdminViewOrderDto
    {
        public string TransactionId { get; set; }
        public int OrderId { get; set; }
        public decimal? TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public virtual List<Address> deliveryAddress { get; set; }
         public List<OrderItemDto> Items { get; set; }

    }
}