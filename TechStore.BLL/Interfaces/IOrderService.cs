using System.Collections.Generic;
using TechStore.DTO;

namespace TechStore.BLL.Interfaces
{
    public interface IOrderService
    {
        void CreateOrder(int userId, List<CartItems> items);
        List<Order> GetUserOrders(int userId);
        Order GetOrderById(int orderId);
    }
}