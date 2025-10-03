using System.Linq;
using TechStore.DTO;
using TechStore.DAL.Interfaces;

namespace TechStore.DAL
{
    public class CategoryDAL : GenericDAL<Category>, ICategoryDAL
    {
        public Category GetByName(string name)
        {
            return _data.FirstOrDefault(c => c.CategoryName == name);
        }
    }
}
