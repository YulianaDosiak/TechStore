using NUnit.Framework;
using Moq;
using TechStore.BLL.Concrete;
using TechStore.DAL.Interfaces;
using TechStore.DTO;
using System;

namespace TechStore.BLL.Tests
{
    [TestFixture]
    public class ProductServiceTests
    {
        private Mock<IProductDAL> _mockProductDal;
        private ProductService _productService;

        [SetUp]
        public void Setup()
        {
            _mockProductDal = new Mock<IProductDAL>();
            _productService = new ProductService(_mockProductDal.Object);
        }

        [Test]
        public void AddProduct_ValidData_CallsCreate()
        {
            var product = new Product { ProductName = "Phone", Price = 1000 };
            _productService.AddProduct(product);
            _mockProductDal.Verify(dal => dal.Create(product), Times.Once);
        }

        [Test]
        public void AddProduct_NegativePrice_ThrowsException()
        {
            var product = new Product { ProductName = "Phone", Price = -500 };

            Assert.Throws<ArgumentException>(() => _productService.AddProduct(product));

            _mockProductDal.Verify(dal => dal.Create(It.IsAny<Product>()), Times.Never);
        }

        [Test]
        public void UpdateProduct_ValidData_CallsUpdate()
        {
            var product = new Product { ProductID = 1, Price = 200 };
            _productService.UpdateProduct(product);
            _mockProductDal.Verify(dal => dal.Update(product), Times.Once);
        }
    }
}