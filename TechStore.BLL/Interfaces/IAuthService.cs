using TechStore.DTO;

namespace TechStore.BLL.Interfaces
{
    public interface IAuthService
    {
        User Login(string username, string password);
        bool Register(User newUser);
    }
}