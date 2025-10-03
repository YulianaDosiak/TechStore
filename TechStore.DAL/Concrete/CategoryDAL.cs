using System.Linq;
using TechStore.DTO;           // тут вже є Category
using TechStore.DAL.Interfaces;

namespace TechStore.DAL
{
    public class CategoryDAL : GenericDAL<Category>, ICategoryDAL
    {
        public Category GetByName(string name)
        {
            // _data припускаємо — це список Category у GenericDAL
            return _data.FirstOrDefault(c => c.CategoryName == name);
        }
    }
}
