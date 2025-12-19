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
    public class CartItemsDALEFTests
    {
        private string _testConnectionString;
        private IMapper _mapper;
        private CartItemsDALEF _dal;

        private CartDALEF _cartDal;
        private UserDALEF _userDal;
        private ProductDALEF _productDal;
        private CategoryDALEF _categoryDal;

        private int _testUserId;
        private int _testCategoryId;
        private int _testProductId;
        private int _testCartId;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            _testConnectionString = config.GetConnectionString("TestConnection");

            var configExpression = new MapperConfigurationExpression();
            configExpression.AddProfile<CartItemsMap>();
            configExpression.AddProfile<CartMap>();
            configExpression.AddProfile<ProductMap>();
            configExpression.AddProfile<UserMap>();
            configExpression.AddProfile<CategoryMap>();
            var mapperConfig = new MapperConfiguration(configExpression);

            _mapper = mapperConfig.CreateMapper();
            _mapper = mapperConfig.CreateMapper();

            _dal = new CartItemsDALEF(_testConnectionString, _mapper);
            _cartDal = new CartDALEF(_testConnectionString, _mapper);
            _userDal = new UserDALEF(_testConnectionString, _mapper);
            _productDal = new ProductDALEF(_testConnectionString, _mapper);
            _categoryDal = new CategoryDALEF(_testConnectionString, _mapper);

            var user = _userDal.Create(new User { Username = "TestUserForCI", Email = "ci@test.com", Password = "123" });
            _testUserId = user.UserID;

            var cat = _categoryDal.Create(new Category { CategoryName = "TestCatForCI" });
            _testCategoryId = cat.CategoryID;

            var prod = _productDal.Create(new Product { Productname = "TestProdForCI", CategoryID = _testCategoryId, Price = 10, Quantity = 50 });
            _testProductId = prod.ProductID;

            var cart = _cartDal.Create(new Cart { UserID = _testUserId });
            _testCartId = cart.CartID;
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            if (_testCartId > 0) _cartDal.Delete(_testCartId);
            if (_testProductId > 0) _productDal.Delete(_testProductId);
            if (_testCategoryId > 0) _categoryDal.Delete(_testCategoryId);
            if (_testUserId > 0) _userDal.Delete(_testUserId);
        }

        [Test]
        public void InsertCartItem_WorksCorrectly()
        {
            var item = new CartItems
            {
                CartID = _testCartId,
                ProductID = _testProductId,
                Quantity = 2
            };

            var created = _dal.Create(item);
            Assert.That(created, Is.Not.Null);
            Assert.That(created.Quantity, Is.EqualTo(2));
            _dal.Delete(created.CartItemID);
        }

        [Test]
        public void UpdateCartItem_WorksCorrectly()
        {
            var item = new CartItems
            {
                CartID = _testCartId,
                ProductID = _testProductId,
                Quantity = 5
            };

            var created = _dal.Create(item);
            Assert.That(created, Is.Not.Null);

            created.Quantity = 10;
            var updated = _dal.Update(created);
            Assert.That(updated, Is.Not.Null);
            Assert.That(updated.Quantity, Is.EqualTo(10));
            _dal.Delete(updated.CartItemID);
        }

        [Test]
        public void DeleteCartItem_WorksCorrectly()
        {
            var item = new CartItems
            {
                CartID = _testCartId,
                ProductID = _testProductId,
                Quantity = 3
            };

            var created = _dal.Create(item);
            Assert.That(created, Is.Not.Null);

            var deleted = _dal.Delete(created.CartItemID);
            Assert.That(deleted, Is.True);

            var fromDb = _dal.GetById(created.CartItemID);
            Assert.That(fromDb, Is.Null);
        }
    }
}