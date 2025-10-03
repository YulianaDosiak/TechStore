using TechStore.DTO;

namespace TechStore.DAL.Interfaces
{
    public interface IUserDAL : IGenericDAL<User>
    {
        User GetByUsername(string username);
        User GetByEmail(string email);
    }
}
