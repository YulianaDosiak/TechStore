namespace TechStore.DTO
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public List<Order>? Orders { get; set; }
        public Cart? Cart { get; set; }
    }
}
