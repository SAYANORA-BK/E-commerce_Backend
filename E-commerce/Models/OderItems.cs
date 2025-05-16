namespace E_commerce.Models
{
    public class OderItems
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int productId { get; set; }
        public decimal? TotalPrice { get; set; }
        public int Quantity { get; set; }
        public virtual Product? Product { get; set; }

        public virtual Order? Order { get; set; }
        public string ProductName { get; internal set; }
    }
}
