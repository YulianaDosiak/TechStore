using NUnit.Framework;
using Moq;
using TechStore.BLL.Concrete;
using TechStore.DAL.Interfaces;
using TechStore.DTO;
using System.Collections.Generic;

namespace TechStore.BLL.Tests
{
    [TestFixture]
    public class CategoryServiceTests
    {
        private Mock<ICategoryDAL> _mockCategoryDal;
        private CategoryService _categoryService;

        [SetUp]
        public void Setup()
        {
            _mockCategoryDal = new Mock<ICategoryDAL>();
            _categoryService = new CategoryService(_mockCategoryDal.Object);
        }

        [Test]
        public void AddCategory_CallsCreateInDal()
        {
            var category = new Category { CategoryName = "Laptops" };
            _categoryService.AddCategory(category);
            _mockCategoryDal.Verify(dal => dal.Create(category), Times.Once);
        }

        [Test]
        public void GetAllCategories_ReturnsList()
        {
            var list = new List<Category> { new Category { CategoryName = "A" }, new Category { CategoryName = "B" } };
            _mockCategoryDal.Setup(dal => dal.GetAll()).Returns(list);

            var result = _categoryService.GetAllCategories();

            Assert.That(result.Count, Is.EqualTo(2));
        }
    }
}