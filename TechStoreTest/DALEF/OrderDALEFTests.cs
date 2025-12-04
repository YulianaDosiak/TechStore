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
    public class OrderDALEFTests
    {
        private string _testConnectionString;
        private IMapper _mapper;
        private OrderDALEF _dal;
        private UserDALEF _userDal;
        private int _testUserId;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            _testConnectionString = config.GetConnectionString("TestConnection");

            var configExpression = new MapperConfigurationExpression();
            configExpression.AddProfile<OrderMap>();
            configExpression.AddProfile<UserMap>();
            var mapperConfig = new MapperConfiguration(configExpression);

            _mapper = mapperConfig.CreateMapper();
            _mapper = mapperConfig.CreateMapper();

            _dal = new OrderDALEF(_testConnectionString, _mapper);
            _userDal = new UserDALEF(_testConnectionString, _mapper);

            var user = _userDal.Create(new User { Username = "TestUserForOrders", Email = "order@test.com", Password = "123" });
            _testUserId = user.UserID;
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            if (_testUserId > 0)
            {
                _userDal.Delete(_testUserId);
            }
        }

        [Test]
        public void GetAllOrders_ReturnsOrders()
        {
            var list = _dal.GetAll();
            Assert.That(list, Is.Not.Null);
            Assert.That(list.Count, Is.GreaterThanOrEqualTo(0));
        }

        [Test]
        public void InsertOrder_WorksCorrectly()
        {
            var order = new Order
            {
                UserID = _testUserId,
                OrderDate = DateTime.Now,
                TotalAmount = 150
            };

            var created = _dal.Create(order);
            Assert.That(created, Is.Not.Null);
            Assert.That(created.TotalAmount, Is.EqualTo(150));
            _dal.Delete(created.OrderID);
        }

        [Test]
        public void UpdateOrder_WorksCorrectly()
        {
            var order = new Order
            {
                UserID = _testUserId,
                OrderDate = DateTime.Now,
                TotalAmount = 100
            };

            var created = _dal.Create(order);
            Assert.That(created, Is.Not.Null);

            created.TotalAmount = 250;
            var updated = _dal.Update(created);
            Assert.That(updated, Is.Not.Null);
            Assert.That(updated.TotalAmount, Is.EqualTo(250));
            _dal.Delete(updated.OrderID);
        }

        [Test]
        public void DeleteOrder_WorksCorrectly()
        {
            var order = new Order
            {
                UserID = _testUserId,
                OrderDate = DateTime.Now,
                TotalAmount = 300
            };

            var created = _dal.Create(order);
            Assert.That(created, Is.Not.Null);

            var deleted = _dal.Delete(created.OrderID);
            Assert.That(deleted, Is.True);

            var fromDb = _dal.GetById(created.OrderID);
            Assert.That(fromDb, Is.Null);
        }
    }
}