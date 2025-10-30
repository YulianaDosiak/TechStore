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
    public class CartDALEFTests
    {
        private string _testConnectionString;
        private IMapper _mapper;
        private CartDALEF _dal;
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
            configExpression.AddProfile<CartMap>();
            configExpression.AddProfile<UserMap>();
            var mapperConfig = new MapperConfiguration(configExpression, NullLoggerFactory.Instance);
            _mapper = mapperConfig.CreateMapper();

            _dal = new CartDALEF(_testConnectionString, _mapper);
            _userDal = new UserDALEF(_testConnectionString, _mapper);

            var user = _userDal.Create(new User { Username = "TestUserForCart", Email = "cart@test.com", Password = "123" });
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
        public void GetAllCarts_ReturnsCarts()
        {
            var list = _dal.GetAll();
            Assert.That(list, Is.Not.Null);
            Assert.That(list.Count, Is.GreaterThanOrEqualTo(0));
        }

        [Test]
        public void InsertCart_WorksCorrectly()
        {
            var cart = new Cart
            {
                UserID = _testUserId,
            };

            var created = _dal.Create(cart);
            Assert.That(created, Is.Not.Null);
            Assert.That(created.CartID, Is.GreaterThan(0));
            _dal.Delete(created.CartID);
        }

        [Test]
        public void DeleteCart_WorksCorrectly()
        {
            var cart = new Cart
            {
                UserID = _testUserId,
            };

            var created = _dal.Create(cart);
            Assert.That(created, Is.Not.Null);

            var deleted = _dal.Delete(created.CartID);
            Assert.That(deleted, Is.True);

            var fromDb = _dal.GetById(created.CartID);
            Assert.That(fromDb, Is.Null);
        }
    }
}