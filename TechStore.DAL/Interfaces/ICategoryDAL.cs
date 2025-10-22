using TechStore.DTO;
using System.Collections.Generic;

namespace TechStore.DAL.Interfaces
{
    public interface ICategoryDAL
    {
        IEnumerable<Category> GetAll();
        Category GetById(int id);
        void Insert(Category category);
        void Update(Category category);
        void Delete(int id);
    }
}