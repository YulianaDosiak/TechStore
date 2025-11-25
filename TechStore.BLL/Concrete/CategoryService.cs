using System.Collections.Generic;
using TechStore.BLL.Interfaces;
using TechStore.DAL.Interfaces;
using TechStore.DTO;

namespace TechStore.BLL.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryDAL _categoryDal;

        public CategoryService(ICategoryDAL categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public List<Category> GetAllCategories()
        {
            return _categoryDal.GetAll();
        }

        public Category GetCategoryById(int id)
        {
            return _categoryDal.GetById(id);
        }

        public void AddCategory(Category category)
        {
            _categoryDal.Create(category);
        }

        public void UpdateCategory(Category category)
        {
            _categoryDal.Update(category);
        }

        public void DeleteCategory(int id)
        {
            _categoryDal.Delete(id);
        }
    }
}