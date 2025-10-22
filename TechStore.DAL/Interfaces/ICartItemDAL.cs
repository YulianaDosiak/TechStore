using TechStore.DTO;
using System.Collections.Generic;

namespace TechStore.DAL.Interfaces
{
    public interface ICartItemDAL
    {
        IEnumerable<CartItem> GetAll();
        CartItem GetById(int id);
        void Insert(CartItem item);
        void Update(CartItem item);
        void Delete(int id);
    }
}