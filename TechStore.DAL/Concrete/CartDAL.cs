using System.Linq;
using TechStore.DTO;
using TechStore.DAL.Interfaces;

namespace TechStore.DAL
{
    public class CartDAL : GenericDAL<Cart>, ICartDAL
    {
        public Cart GetByUserId(int userId)
        {
            return _data.FirstOrDefault(c => c.UserId == userId);
        }
    }
}
