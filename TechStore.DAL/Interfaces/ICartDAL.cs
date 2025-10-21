using TechStore.DTO;
using System.Collections.Generic;

namespace TechStore.DAL.Interfaces
{
    public interface ICartDAL
    {
        IEnumerable<Cart> GetAll();
        Cart GetById(int id);
        void Insert(Cart cart);
        void Update(Cart cart);
        void Delete(int id);
    }
}
