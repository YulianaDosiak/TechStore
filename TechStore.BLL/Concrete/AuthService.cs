using System;
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

            if (users == null || !users.Any()) return null;

            string passwordHash = PasswordHasher.HashPassword(password);

            var user = users.FirstOrDefault(u =>
                u.Username != null &&
                u.Username.Trim().Equals(username.Trim(), StringComparison.OrdinalIgnoreCase));

            if (user != null && user.Password == passwordHash)
            {
                return user;
            }

            return null;
        }

        public bool Register(User newUser)
        {
            var users = _userDal.GetAll();

            if (users.Any(u => u.Username.Trim().Equals(newUser.Username.Trim(), StringComparison.OrdinalIgnoreCase)))
            {
                return false;
            }

            newUser.Password = PasswordHasher.HashPassword(newUser.Password);
            _userDal.Create(newUser);
            return true;
        }
    }
}