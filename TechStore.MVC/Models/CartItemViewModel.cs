namespace TechStore.MVC.Models
{
    public class CartItemViewModel
    {
        public int CartItemId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}