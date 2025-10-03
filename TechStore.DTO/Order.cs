namespace TechStore.DTO
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public decimal TotalAmount { get; set; }

        // FK
        public int UserId { get; set; }

        // Навігація
        public User? User { get; set; }
        public List<OrderItem>? OrderItems { get; set; }
    }
}
