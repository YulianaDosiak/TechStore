namespace TechStore.DTO
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }                // хто зробив замовлення
        public DateTime OrderDate { get; set; }
        public bool IsActive { get; set; }             // 🔹 потрібно у DAL
    }
}
