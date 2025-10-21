using TechStore.DTO;
using System.Collections.Generic;

namespace TechStore.DAL.Interfaces
{
    public interface IProductDAL
    {
        IEnumerable<Product> GetAll();
        Product GetById(int id);
        void Insert(Product product);
        void Update(Product product);
        void Delete(int id);
    }
}
