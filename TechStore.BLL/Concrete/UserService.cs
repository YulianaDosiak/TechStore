using System.Collections.Generic;
using TechStore.BLL.Interfaces;
using TechStore.DAL.Interfaces;
using TechStore.DTO;

namespace TechStore.BLL.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserDAL _userDal;

        public UserService(IUserDAL userDal)
        {
            _userDal = userDal;
        }

        public List<User> GetAllUsers()
        {
            return _userDal.GetAll();
        }

        public User GetUserById(int id)
        {
            return _userDal.GetById(id);
        }

        public void UpdateUser(User user)
        {
            _userDal.Update(user);
        }

        public void DeleteUser(int id)
        {
            _userDal.Delete(id);
        }
    }
}