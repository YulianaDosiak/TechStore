using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using TechStore.BLL.Interfaces;
using TechStore.DTO;
using TechStore.MVC.Controllers;
using Xunit;

namespace TechStore.MVC.Tests
{
    public class ProductControllerTests
    {
        private readonly Mock<IProductService> _mockProductService;
        private readonly Mock<ICategoryService> _mockCategoryService;
        private readonly ProductController _controller;

        public ProductControllerTests()
        {
            _mockProductService = new Mock<IProductService>();
            _mockCategoryService = new Mock<ICategoryService>();
            _controller = new ProductController(_mockProductService.Object, _mockCategoryService.Object);
        }

        [Fact]
        public void Index_ReturnsView_WithListOfCategories()
        {
            // Arrange
            var categories = new List<Category>
            {
                new Category { CategoryID = 1, CategoryName = "Laptops" },
                new Category { CategoryID = 2, CategoryName = "Phones" }
            };
            _mockCategoryService.Setup(s => s.GetAllCategories()).Returns(categories);

            // Act
            var result = _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Category>>(viewResult.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public void List_ReturnsFilteredProducts_ByCategoryId()
        {
            // Arrange
            int categoryId = 1;
            var products = new List<Product>
            {
                new Product { ProductID = 1, CategoryID = 1, ProductName = "Laptop A", Price = 1000 },
                new Product { ProductID = 2, CategoryID = 2, ProductName = "Phone B", Price = 500 }, // Інша категорія
                new Product { ProductID = 3, CategoryID = 1, ProductName = "Laptop C", Price = 1200 }
            };

            _mockProductService.Setup(s => s.GetAllProducts()).Returns(products);

            // Act
            var result = _controller.List(categoryId, null, null);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<Product>>(viewResult.Model);

            Assert.Equal(2, model.Count); // Має бути тільки 2 товари з категорії 1
            Assert.All(model, p => Assert.Equal(categoryId, p.CategoryID));
        }

        [Fact]
        public void List_ReturnsFilteredProducts_BySearchString()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { ProductID = 1, CategoryID = 1, ProductName = "Super Laptop" },
                new Product { ProductID = 2, CategoryID = 1, ProductName = "Gaming Mouse" }
            };

            _mockProductService.Setup(s => s.GetAllProducts()).Returns(products);

            // Act
            // Шукаємо "Mouse" в категорії 1
            var result = _controller.List(1, null, "Mouse");

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<Product>>(viewResult.Model);

            Assert.Single(model); // Тільки 1 товар має знайтися
            Assert.Equal("Gaming Mouse", model.First().ProductName);
        }

        [Fact]
        public void List_SortsProducts_ByPriceDescending()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { ProductID = 1, CategoryID = 1, ProductName = "Cheap", Price = 100 },
                new Product { ProductID = 2, CategoryID = 1, ProductName = "Expensive", Price = 900 }
            };

            _mockProductService.Setup(s => s.GetAllProducts()).Returns(products);

            // Act
            // Сортуємо "price_desc"
            var result = _controller.List(1, "price_desc", null);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<Product>>(viewResult.Model);

            Assert.Equal(900, model[0].Price); // Перший має бути дорожчий
            Assert.Equal(100, model[1].Price);
        }
    }
}