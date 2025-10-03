namespace TechStore.DTO
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        // FK
        public int OrderId { get; set; }
        public int ProductId { get; set; }

        // Навігація
        public Order? Order { get; set; }
        public Product? Product { get; set; }
    }
}
