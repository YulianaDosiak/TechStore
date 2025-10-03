using TechStore.DTO;

namespace TechStore.DAL.Interfaces
{
    public interface ICategoryDAL : IGenericDAL<Category>
    {
        Category GetByName(string name);
    }
}
