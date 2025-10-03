namespace TechStore.DTO
{
    public class CartItem
    {
        public int CartItemId { get; set; }
        public int Quantity { get; set; }

        // FK
        public int CartId { get; set; }
        public int ProductId { get; set; }

        // Навігація
        public Cart? Cart { get; set; }
        public Product? Product { get; set; }
    }
}
