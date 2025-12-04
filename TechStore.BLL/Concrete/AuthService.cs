using System.Linq;
using TechStore.BLL.Helpers;
using TechStore.BLL.Interfaces;
using TechStore.DAL.Interfaces;
using TechStore.DTO;

namespace TechStore.BLL.Concrete
{
    public class AuthService : IAuthService
    {
        private readonly IUserDAL _userDal;

        public AuthService(IUserDAL userDal)
        {
            _userDal = userDal;
        }

        public User Login(string username, string password)
        {
            var users = _userDal.GetAll();

            string passwordHash = PasswordHasher.HashPassword(password);

            return users.FirstOrDefault(u => u.Username == username && u.Password == passwordHash);
        }

        public bool Register(User newUser)
        {
            var users = _userDal.GetAll();
            if (users.Any(u => u.Username == newUser.Username))
            {
                return false;
            }

            newUser.Password = PasswordHasher.HashPassword(newUser.Password);

            _userDal.Create(newUser);
            return true;
        }
    }
}