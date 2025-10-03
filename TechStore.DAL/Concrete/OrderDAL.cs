using System.Collections.Generic;
using System.Linq;
using TechStore.DTO;
using TechStore.DAL.Interfaces;

namespace TechStore.DAL
{
    public class OrderDAL : GenericDAL<Order>, IOrderDAL
    {
        public List<Order> GetByUserId(int userId)
        {
            return _data.Where(o => o.UserId == userId).ToList();
        }

        public List<Order> GetActiveOrders()
        {
            return _data.Where(o => o.IsActive).ToList();
        }
    }
}
