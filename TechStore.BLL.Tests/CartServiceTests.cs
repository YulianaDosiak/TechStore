using NUnit.Framework;
using Moq;
using TechStore.BLL.Concrete;
using TechStore.DAL.Interfaces;
using TechStore.DTO;
using System.Collections.Generic;

namespace TechStore.BLL.Tests
{
    [TestFixture]
    public class CartServiceTests
    {
        private Mock<ICartDAL> _mockCartDal;
        private Mock<ICartItemsDAL> _mockCartItemsDal;
        private Mock<IProductDAL> _mockProductDal;
        private CartService _cartService;

        [SetUp]
        public void Setup()
        {
            _mockCartDal = new Mock<ICartDAL>();
            _mockCartItemsDal = new Mock<ICartItemsDAL>();
            _mockProductDal = new Mock<IProductDAL>();
            _cartService = new CartService(_mockCartDal.Object, _mockCartItemsDal.Object, _mockProductDal.Object);
        }

        [Test]
        public void GetCartByUserId_CartExists_ReturnsCart()
        {
            var userId = 1;
            var existingCart = new Cart { CartID = 55, UserID = userId };
            _mockCartDal.Setup(x => x.GetAll()).Returns(new List<Cart> { existingCart });

            var result = _cartService.GetCartByUserId(userId);

            Assert.That(result.CartID, Is.EqualTo(55));
        }

        [Test]
        public void GetCartByUserId_NoCart_CreatesNew()
        {
            var userId = 2;
            _mockCartDal.Setup(x => x.GetAll()).Returns(new List<Cart>()); // Порожній список
            _mockCartDal.Setup(x => x.Create(It.IsAny<Cart>())).Returns(new Cart { CartID = 99, UserID = userId });

            var result = _cartService.GetCartByUserId(userId);
            _mockCartDal.Verify(x => x.Create(It.Is<Cart>(c => c.UserID == userId)), Times.Once);
            Assert.That(result.CartID, Is.EqualTo(99));
        }

        [Test]
        public void AddItemToCart_ProductExists_AddsItemWithoutPrice()
        {
            var userId = 1;
            var productId = 10;
            var product = new Product { ProductID = productId, Productname = "Test" };
            var cart = new Cart { CartID = 1, UserID = userId };

            _mockCartDal.Setup(x => x.GetAll()).Returns(new List<Cart> { cart });
            _mockProductDal.Setup(x => x.GetById(productId)).Returns(product);

            _cartService.AddItemToCart(userId, productId, 5);

            _mockCartItemsDal.Verify(x => x.Create(It.Is<CartItems>(ci =>
                ci.CartID == 1 &&
                ci.ProductID == productId &&
                ci.Quantity == 5
            )), Times.Once);
        }
    }
}