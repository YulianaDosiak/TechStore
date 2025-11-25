namespace TechStore.DTO
{
    public class CartItems
    {
        public int CartItemID { get; set; }
        public int CartID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }

        public string? ProductName { get; set; }
        public decimal Price { get; set; }

        public override string ToString()
        {
            return $"CartItemID: {CartItemID}, Product: {ProductName}, Qty: {Quantity}";
        }
    }
}