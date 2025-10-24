using NUnit.Framework;
using System;
using System.Linq;
using System.Transactions;
using TechStore.DAL.Concrete;
using TechStore.DTO;

namespace TechStore.DAL.Tests
{
    [TestFixture]
    public class ProductDALTests
    {
        private ProductDAL _dal;
        private TransactionScope _scope;

        [SetUp]
        public void Setup()
        {
            var context = new TechStoreDbContext(
                "Data Source=localhost;Initial Catalog=TechStoreDB;Integrated Security=True;");
            _dal = new ProductDAL(context);
            _scope = new TransactionScope(); // Всі зміни відкотяться після тесту
        }

        [TearDown]
        public void TearDown()
        {
            _scope.Dispose();
        }

        [Test]
        public void InsertAndGetById_ShouldWork()
        {
            var product = new Product { ProductName = "TestProduct", Price = 10m, Quantity = 5, CategoryID = 1 };
            _dal.Insert(product);

            var inserted = _dal.GetAll().Last();
            Assert.IsNotNull(inserted);
            Assert.AreEqual("TestProduct", inserted.ProductName);
        }

        [Test]
        public void Update_ShouldModifyProduct()
        {
            var product = new Product { ProductName = "Old", Price = 5m, Quantity = 1, CategoryID = 1 };
            _dal.Insert(product);
            var inserted = _dal.GetAll().Last();

            inserted.ProductName = "New";
            inserted.Price = 15m;
            _dal.Update(inserted);

            var updated = _dal.GetById(inserted.ProductID);
            Assert.AreEqual("New", updated.ProductName);
            Assert.AreEqual(15m, updated.Price);
        }

        [Test]
        public void Delete_ShouldRemoveProduct()
        {
            var product = new Product { ProductName = "ToDelete", Price = 1m, Quantity = 1, CategoryID = 1 };
            _dal.Insert(product);
            var inserted = _dal.GetAll().Last();

            _dal.Delete(inserted.ProductID);
            var deleted = _dal.GetById(inserted.ProductID);
            Assert.IsNull(deleted);
        }
    }
}
