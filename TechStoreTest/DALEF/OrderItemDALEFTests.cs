using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;
using System;
using System.Linq;
using TechStore.DALEF.AutoMapper;
using TechStore.DALEF.Concrete;
using TechStore.DTO;

namespace TechStore.Test.DALEF
{
    [TestFixture]
    public class OrderItemDALEFTests
    {
        private string _testConnectionString;
        private IMapper _mapper;
        private OrderItemDALEF _dal;

        private OrderDALEF _orderDal;
        private UserDALEF _userDal;
        private ProductDALEF _productDal;
        private CategoryDALEF _categoryDal;

        private int _testUserId;
        private int _testCategoryId;
        private int _testProductId;
        private int _testOrderId;


        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            _testConnectionString = config.GetConnectionString("TestConnection");

            var configExpression = new MapperConfigurationExpression();
            configExpression.AddProfile<OrderItemMap>();
            configExpression.AddProfile<OrderMap>();
            configExpression.AddProfile<ProductMap>();
            configExpression.AddProfile<UserMap>();
            configExpression.AddProfile<CategoryMap>();
            var mapperConfig = new MapperConfiguration(configExpression);

            _mapper = mapperConfig.CreateMapper();
            _mapper = mapperConfig.CreateMapper();

            _dal = new OrderItemDALEF(_testConnectionString, _mapper);
            _orderDal = new OrderDALEF(_testConnectionString, _mapper);
            _userDal = new UserDALEF(_testConnectionString, _mapper);
            _productDal = new ProductDALEF(_testConnectionString, _mapper);
            _categoryDal = new CategoryDALEF(_testConnectionString, _mapper);

            var user = _userDal.Create(new User { Username = "TestUserForOI", Email = "oi@test.com", Password = "123" });
            _testUserId = user.UserID;

            var cat = _categoryDal.Create(new Category { CategoryName = "TestCatForOI" });
            _testCategoryId = cat.CategoryID;

            var prod = _productDal.Create(new Product { Productname = "TestProdForOI", CategoryID = _testCategoryId, Price = 99, Quantity = 100 });
            _testProductId = prod.ProductID;

            var order = _orderDal.Create(new Order { UserID = _testUserId, OrderDate = DateTime.Now, TotalAmount = 500 });
            _testOrderId = order.OrderID;
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            if (_testOrderId > 0) _orderDal.Delete(_testOrderId);
            if (_testProductId > 0) _productDal.Delete(_testProductId);
            if (_testCategoryId > 0) _categoryDal.Delete(_testCategoryId);
            if (_testUserId > 0) _userDal.Delete(_testUserId);
        }

        [Test]
        public void GetAllOrderItems_ReturnsItems()
        {
            var list = _dal.GetAll();
            Assert.That(list, Is.Not.Null);
            Assert.That(list.Count, Is.GreaterThanOrEqualTo(0));
        }

        [Test]
        public void InsertOrderItem_WorksCorrectly()
        {
            var item = new OrderItem
            {
                OrderID = _testOrderId,
                ProductID = _testProductId,
                Quantity = 5,
                Price = 99
            };

            var created = _dal.Create(item);
            Assert.That(created, Is.Not.Null);
            Assert.That(created.Quantity, Is.EqualTo(5));
            _dal.Delete(created.OrderItemID);
        }

        [Test]
        public void UpdateOrderItem_WorksCorrectly()
        {
            var item = new OrderItem
            {
                OrderID = _testOrderId,
                ProductID = _testProductId,
                Quantity = 10,
                Price = 99
            };

            var created = _dal.Create(item);
            Assert.That(created, Is.Not.Null);

            created.Quantity = 20;
            var updated = _dal.Update(created);
            Assert.That(updated, Is.Not.Null);
            Assert.That(updated.Quantity, Is.EqualTo(20));
            _dal.Delete(updated.OrderItemID);
        }

        [Test]
        public void DeleteOrderItem_WorksCorrectly()
        {
            var item = new OrderItem
            {
                OrderID = _testOrderId,
                ProductID = _testProductId,
                Quantity = 15,
                Price = 99
            };

            var created = _dal.Create(item);
            Assert.That(created, Is.Not.Null);

            var deleted = _dal.Delete(created.OrderItemID);
            Assert.That(deleted, Is.True);

            var fromDb = _dal.GetById(created.OrderItemID);
            Assert.That(fromDb, Is.Null);
        }
    }
}