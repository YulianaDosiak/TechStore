using TechStore.DTO;
using System.Collections.Generic;

namespace TechStore.DAL.Interfaces
{
    public interface IOrderDAL : IGenericDAL<Order>
    {
        List<Order> GetByUserId(int userId);
        List<Order> GetActiveOrders();
    }
}
