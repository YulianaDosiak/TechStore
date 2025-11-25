using TechStore.DTO;

namespace TechStore.WPF.Services
{
    public class UserSession
    {
        public User? CurrentUser { get; private set; }

        public void SetUser(User user)
        {
            CurrentUser = user;
        }

        public void Clear()
        {
            CurrentUser = null;
        }

        public bool IsLoggedIn => CurrentUser != null;
    }
}