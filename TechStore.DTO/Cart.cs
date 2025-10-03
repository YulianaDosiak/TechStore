namespace TechStore.DTO
{
    public class Cart
    {
        public int CartId { get; set; }

        // FK
        public int UserId { get; set; }

        // Навігація
        public User? User { get; set; }
        public List<CartItem>? CartItems { get; set; }
    }
}
