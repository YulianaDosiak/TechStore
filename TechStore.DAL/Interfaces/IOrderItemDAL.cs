using TechStore.DTO;
using System.Collections.Generic;

namespace TechStore.DAL.Interfaces
{
    public interface IOrderItemDAL
    {
        IEnumerable<OrderItem> GetAll();
        OrderItem GetById(int id);
        void Insert(OrderItem item);
        void Update(OrderItem item);
        void Delete(int id);
    }
}
