using TechStore.DTO;

namespace TechStore.DAL.Interfaces
{
    public interface ICartDAL : IGenericDAL<Cart>
    {
        Cart GetByUserId(int userId);
    }
}
