using NUnit.Framework;
using System;
using System.Linq;
using System.Transactions;
using TechStore.DAL.Concrete;
using TechStore.DTO;

namespace TechStore.DAL.Tests
{
    [TestFixture]
    public class OrderDALTests
    {
        private OrderDAL _orderDal;
        private UserDAL _userDal;
        private TransactionScope _scope;

        [SetUp]
        public void Setup()
        {
            var context = new TechStoreDbContext(
                "Data Source=localhost;Initial Catalog=TechStoreDB;Integrated Security=True;");
            _orderDal = new OrderDAL(context);
            _userDal = new UserDAL(context);
            _scope = new TransactionScope();
        }

        [TearDown]
        public void TearDown() => _scope.Dispose();

        private int CreateTestUser()
        {
            var user = new User { Username = "TestUser", Email = "a@b.com", Password = "123" };
            _userDal.Insert(user);
            return _userDal.GetAll().Last().UserID;
        }

        [Test]
        public void InsertAndGetById_ShouldWork()
        {
            int userId = CreateTestUser();
            var order = new Order { UserID = userId, OrderDate = DateTime.Now, TotalAmount = 100m };
            _orderDal.Insert(order);

            var inserted = _orderDal.GetAll().Last();
            Assert.IsNotNull(inserted);
            Assert.AreEqual(100m, inserted.TotalAmount);
        }

        [Test]
        public void Update_ShouldModifyOrder()
        {
            int userId = CreateTestUser();
            var order = new Order { UserID = userId, OrderDate = DateTime.Now, TotalAmount = 50m };
            _orderDal.Insert(order);
            var inserted = _orderDal.GetAll().Last();

            inserted.TotalAmount = 200m;
            _orderDal.Update(inserted);

            var updated = _orderDal.GetById(inserted.OrderID);
            Assert.AreEqual(200m, updated.TotalAmount);
        }

        [Test]
        public void Delete_ShouldRemoveOrder()
        {
            int userId = CreateTestUser();
            var order = new Order { UserID = userId, OrderDate = DateTime.Now, TotalAmount = 75m };
            _orderDal.Insert(order);
            var inserted = _orderDal.GetAll().Last();

            _orderDal.Delete(inserted.OrderID);
            var deleted = _orderDal.GetById(inserted.OrderID);
            Assert.IsNull(deleted);
        }
    }
}
