using System.Collections.Generic;
using System.Linq;
using TechStore.DTO;
using TechStore.DAL.Interfaces;

namespace TechStore.DAL
{
    public class ProductDAL : GenericDAL<Product>, IProductDAL
    {
        public List<Product> GetByCategory(int categoryId)
        {
            return _data.Where(p => p.CategoryId == categoryId).ToList();
        }

        public List<Product> SearchByName(string name)
        {
            return _data.Where(p => p.Name.Contains(name)).ToList();
        }
    }
}
