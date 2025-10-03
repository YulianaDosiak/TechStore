namespace TechStore.DTO
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }

        // FK
        public int CategoryId { get; set; }

        // Навігація
        public Category? Category { get; set; }
        public List<OrderItem>? OrderItems { get; set; }
        public List<CartItem>? CartItems { get; set; }
    }
}
