using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using TechStore.BLL.Interfaces;
using TechStore.DTO;
using TechStore.MVC.Controllers;
using TechStore.MVC.Models;
using Xunit;

namespace TechStore.MVC.Tests
{
    public class CategoryControllerTests
    {
        private readonly Mock<ICategoryService> _mockCategoryService;
        private readonly Mock<IProductService> _mockProductService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ILogger<CategoryController>> _mockLogger;
        private readonly CategoryController _controller;

        public CategoryControllerTests()
        {
            _mockCategoryService = new Mock<ICategoryService>();
            _mockProductService = new Mock<IProductService>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<CategoryController>>();

            _controller = new CategoryController(
                _mockCategoryService.Object,
                _mockProductService.Object,
                _mockMapper.Object,
                _mockLogger.Object
            );

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Name, "TestUser")
            }, "mock"));

            _controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            var tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
            _controller.TempData = tempData;
        }

        [Fact]
        public void Index_ReturnsViewResult_WithListOfCategories()
        {
            var dtoList = new List<Category> { new Category { CategoryID = 1, CategoryName = "Phones" } };
            var vmList = new List<CategoryViewModel> { new CategoryViewModel { CategoryID = 1, CategoryName = "Phones" } };

            _mockCategoryService.Setup(s => s.GetAllCategories()).Returns(dtoList);
            _mockProductService.Setup(p => p.GetAllProducts()).Returns(new List<Product>());

            _mockMapper.Setup(m => m.Map<IEnumerable<CategoryViewModel>>(dtoList)).Returns(vmList);

            var result = _controller.Index(null);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<CategoryIndexViewModel>(viewResult.Model);
            Assert.Single(model.Categories);
        }


        [Fact]
        public void Create_Post_RedirectsToIndex_WhenModelIsValid()
        {
            var vm = new CategoryViewModel { CategoryName = "Laptops" };
            var dto = new Category { CategoryName = "Laptops" };

            _mockMapper.Setup(m => m.Map<Category>(vm)).Returns(dto);

            var result = _controller.Create(vm);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
            _mockCategoryService.Verify(s => s.AddCategory(dto), Times.Once);
        }

        [Fact]
        public void Create_Post_ReturnsView_WhenModelIsInvalid()
        {
            _controller.ModelState.AddModelError("CategoryName", "Required");
            var vm = new CategoryViewModel();

            var result = _controller.Create(vm);

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(vm, viewResult.Model);
            _mockCategoryService.Verify(s => s.AddCategory(It.IsAny<Category>()), Times.Never);
        }


        [Fact]
        public void Edit_Get_ReturnsView_WhenCategoryExists()
        {
            int testId = 1;
            var dto = new Category { CategoryID = testId, CategoryName = "Tablets" };
            var vm = new CategoryViewModel { CategoryID = testId, CategoryName = "Tablets" };

            _mockCategoryService.Setup(s => s.GetCategoryById(testId)).Returns(dto);
            _mockMapper.Setup(m => m.Map<CategoryViewModel>(dto)).Returns(vm);

            var result = _controller.Edit(testId);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<CategoryViewModel>(viewResult.Model);
            Assert.Equal("Tablets", model.CategoryName);
        }

        [Fact]
        public void Edit_Get_ReturnsNotFound_WhenCategoryDoesNotExist()
        {
            int testId = 99;
            _mockCategoryService.Setup(s => s.GetCategoryById(testId)).Returns((Category)null);

            var result = _controller.Edit(testId);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Edit_Post_RedirectsToIndex_WhenModelIsValid()
        {
            var vm = new CategoryViewModel { CategoryID = 1, CategoryName = "Updated Name" };
            var dto = new Category { CategoryID = 1, CategoryName = "Updated Name" };

            _mockMapper.Setup(m => m.Map<Category>(vm)).Returns(dto);

            var result = _controller.Edit(vm);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
            _mockCategoryService.Verify(s => s.UpdateCategory(dto), Times.Once);
        }

        [Fact]
        public void Edit_Post_ReturnsView_WhenModelIsInvalid()
        {
            _controller.ModelState.AddModelError("Error", "Some Error");
            var vm = new CategoryViewModel { CategoryID = 1 };

            var result = _controller.Edit(vm);

            var viewResult = Assert.IsType<ViewResult>(result);
            _mockCategoryService.Verify(s => s.UpdateCategory(It.IsAny<Category>()), Times.Never);
        }


        [Fact]
        public void Delete_RedirectsToIndex_WhenSuccess()
        {
            // Arrange
            int id = 1;

            _mockProductService.Setup(p => p.GetAllProducts()).Returns(new List<Product>());

            _mockCategoryService.Setup(s => s.DeleteCategory(id));

            var result = _controller.Delete(id);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);

            _mockCategoryService.Verify(s => s.DeleteCategory(id), Times.Once);
            Assert.False(_controller.TempData.ContainsKey("Error"));
        }

        [Fact]
        public void Delete_SetsTempDataError_WhenProductsExist()
        {
            int id = 1;

            var existingProducts = new List<Product>
            {
                new Product { ProductID = 100, Productname = "Test Product", CategoryID = 1 }
            };
            _mockProductService.Setup(p => p.GetAllProducts()).Returns(existingProducts);

            var result = _controller.Delete(id);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);

            _mockCategoryService.Verify(s => s.DeleteCategory(It.IsAny<int>()), Times.Never);

            Assert.True(_controller.TempData.ContainsKey("Error"));
            Assert.Contains("Неможливо видалити", _controller.TempData["Error"].ToString());
        }

        [Fact]
        public void Delete_SetsTempDataError_WhenServiceThrowsException()
        {
            int id = 1;

            _mockProductService.Setup(p => p.GetAllProducts()).Returns(new List<Product>());

            _mockCategoryService.Setup(s => s.DeleteCategory(id)).Throws(new Exception("Database error"));

            var result = _controller.Delete(id);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
            Assert.True(_controller.TempData.ContainsKey("Error"));
        }
    }
}