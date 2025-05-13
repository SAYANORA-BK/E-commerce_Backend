namespace E_commerce.Dto
{
    public class AdminViewOrderDto
    {
        public string TransactionId { get; set; }
        public int OrderId { get; set; }
        public decimal? TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
