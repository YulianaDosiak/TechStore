using NUnit.Framework;
using Moq;
using TechStore.BLL.Concrete;
using TechStore.DAL.Interfaces;
using TechStore.DTO;
using System.Collections.Generic;
using System;

namespace TechStore.BLL.Tests
{
    [TestFixture]
    public class OrderServiceTests
    {
        private Mock<IOrderDAL> _mockOrderDal;
        private Mock<IOrderItemDAL> _mockOrderItemDal;
        private Mock<IProductDAL> _mockProductDal;
        private OrderService _orderService;

        [SetUp]
        public void Setup()
        {
            _mockOrderDal = new Mock<IOrderDAL>();
            _mockOrderItemDal = new Mock<IOrderItemDAL>();
            _mockProductDal = new Mock<IProductDAL>(); // важливий!!!!!!!!!!!!
            _orderService = new OrderService(_mockOrderDal.Object, _mockOrderItemDal.Object, _mockProductDal.Object);
        }

        [Test]
        public void CreateOrder_CalculatesTotalCorrectly_AndCreatesItems()
        {
            int userId = 1;

            var cartItems = new List<CartItems>
            {
                new CartItems { ProductID = 10, Quantity = 2 },
                new CartItems { ProductID = 20, Quantity = 1 }
            };

            _mockProductDal.Setup(p => p.GetById(10)).Returns(new Product { ProductID = 10, Price = 100 }); // 2 * 100 = 200
            _mockProductDal.Setup(p => p.GetById(20)).Returns(new Product { ProductID = 20, Price = 50 });  // 1 * 50 = 50
                                                                                                            // Разом = 250

            _mockOrderDal.Setup(d => d.Create(It.IsAny<Order>()))
                         .Returns((Order o) => { o.OrderID = 777; return o; });

            _orderService.CreateOrder(userId, cartItems);

            _mockOrderDal.Verify(d => d.Create(It.Is<Order>(o =>
                o.UserID == userId &&
                o.TotalAmount == 250
            )), Times.Once);

            _mockOrderItemDal.Verify(d => d.Create(It.Is<OrderItem>(oi =>
                oi.OrderID == 777 && oi.ProductID == 10 && oi.Price == 100
            )), Times.Once);

            _mockOrderItemDal.Verify(d => d.Create(It.Is<OrderItem>(oi =>
                oi.OrderID == 777 && oi.ProductID == 20 && oi.Price == 50
            )), Times.Once);
        }

        [Test]
        public void CreateOrder_EmptyList_DoesNothing()
        {
            _orderService.CreateOrder(1, new List<CartItems>());
            _mockOrderDal.Verify(d => d.Create(It.IsAny<Order>()), Times.Never);
        }
    }
}