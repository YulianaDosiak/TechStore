using System.Linq;
using TechStore.DTO;
using TechStore.DAL.Interfaces;

namespace TechStore.DAL
{
    public class UserDAL : GenericDAL<User>, IUserDAL
    {
        public User GetByUsername(string username)
        {
            return _data.FirstOrDefault(u => u.Username == username);
        }

        public User GetByEmail(string email)
        {
            return _data.FirstOrDefault(u => u.Email == email);
        }
    }
}
