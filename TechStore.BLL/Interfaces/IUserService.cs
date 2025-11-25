using System.Collections.Generic;
using TechStore.DTO;

namespace TechStore.BLL.Interfaces
{
    public interface IUserService
    {
        List<User> GetAllUsers();
        User GetUserById(int id);
        void UpdateUser(User user);
        void DeleteUser(int id);
    }
}