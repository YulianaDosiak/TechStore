using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Security.Claims;
using TechStore.BLL.Interfaces;
using TechStore.DAL.Interfaces;
using TechStore.DTO;
using TechStore.MVC.Controllers;
using TechStore.MVC.Models;
using Xunit;

namespace TechStore.MVC.Tests
{
    public class CartControllerTests
    {
        private readonly Mock<ICartService> _mockCartService;
        private readonly Mock<IProductService> _mockProductService;
        private readonly Mock<ICartItemsDAL> _mockCartItemsDal;
        private readonly Mock<ICartDAL> _mockCartDal;
        private readonly CartController _controller;

        public CartControllerTests()
        {
            _mockCartService = new Mock<ICartService>();
            _mockProductService = new Mock<IProductService>();
            _mockCartItemsDal = new Mock<ICartItemsDAL>();
            _mockCartDal = new Mock<ICartDAL>();

            // ВИПРАВЛЕНО: Порядок аргументів відповідає контролеру
            _controller = new CartController(
                _mockCartService.Object,     // 1. Service
                _mockCartItemsDal.Object,    // 2. CartItemsDAL
                _mockCartDal.Object,         // 3. CartDAL
                _mockProductService.Object   // 4. ProductService
            );

            // Імітація користувача (User Claims)
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, "1"), // UserID = 1
                new Claim(ClaimTypes.Name, "TestUser")
            }, "mock"));

            _controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };
        }

        [Fact]
        public void Index_ReturnsEmptyView_WhenCartIsNull()
        {
            // Arrange
            _mockCartService.Setup(s => s.GetCartByUserId(1)).Returns((Cart)null);

            // Act
            var result = _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<CartItemViewModel>>(viewResult.Model);
            Assert.Empty(model);
        }

        [Fact]
        public void Add_CreatesNewCart_IfOneDoesNotExist()
        {
            // Arrange
            int productId = 5;
            _mockCartService.Setup(s => s.GetCartByUserId(1)).Returns((Cart)null); // Кошика спочатку немає

            // Налаштовуємо поведінку: коли спитають другий раз, повернемо новий кошик
            var newCart = new Cart { CartID = 10, UserID = 1 };
            _mockCartService.SetupSequence(s => s.GetCartByUserId(1))
                .Returns((Cart)null) // Перший виклик
                .Returns(newCart);   // Другий виклик (після створення)

            // Act
            var result = _controller.Add(productId);

            // Assert
            // Перевіряємо, що викликався метод створення кошика
            _mockCartDal.Verify(d => d.Create(It.Is<Cart>(c => c.UserID == 1)), Times.Once);

            // Перевіряємо, що викликався метод додавання товару
            _mockCartItemsDal.Verify(d => d.Create(It.Is<CartItems>(i => i.ProductID == productId && i.CartID == 10)), Times.Once);

            // Перевіряємо редірект
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
        }

        [Fact]
        public void Remove_DeletesItem_AndRedirects()
        {
            // Arrange
            int cartItemId = 99;

            // Act
            var result = _controller.Remove(cartItemId);

            // Assert
            _mockCartItemsDal.Verify(d => d.Delete(cartItemId), Times.Once);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
        }
    }
}