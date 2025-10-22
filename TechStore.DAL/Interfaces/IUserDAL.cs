using TechStore.DTO;
using System.Collections.Generic;

namespace TechStore.DAL.Interfaces
{
    public interface IUserDAL
    {
        IEnumerable<User> GetAll();
        User GetById(int id);
        void Insert(User user);
        void Update(User user);
        void Delete(int id);
    }
}