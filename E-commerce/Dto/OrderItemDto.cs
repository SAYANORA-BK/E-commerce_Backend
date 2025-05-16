namespace E_commerce.Dto
{
    public class OrderItemDto
    {
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal? TotalPrice { get; set; }
    }
}
