using TechStore.DTO;
using System.Collections.Generic;

namespace TechStore.DAL.Interfaces
{
    public interface IProductDAL : IGenericDAL<Product>
    {
        List<Product> GetByCategory(int categoryId);
        List<Product> SearchByName(string name);
    }
}
