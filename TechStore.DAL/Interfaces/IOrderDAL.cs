using TechStore.DTO;
using System.Collections.Generic;

namespace TechStore.DAL.Interfaces
{
    public interface IOrderDAL
    {
        IEnumerable<Order> GetAll();
        Order GetById(int id);
        void Insert(Order order);
        void Update(Order order);
        void Delete(int id);
    }
}
