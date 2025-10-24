using NUnit.Framework;
using System.Linq;
using System.Transactions;
using TechStore.DAL.Concrete;
using TechStore.DTO;

namespace TechStore.DAL.Tests
{
    [TestFixture]
    public class CartItemDALTests
    {
        private CartItemDAL _dal;
        private CartDAL _cartDal;
        private ProductDAL _productDal;
        private TransactionScope _scope;

        [SetUp]
        public void Setup()
        {
            var context = new TechStoreDbContext(
                "Data Source=localhost;Initial Catalog=TechStoreDB;Integrated Security=True;");
            _dal = new CartItemDAL(context);
            _cartDal = new CartDAL(context);
            _productDal = new ProductDAL(context);
            _scope = new TransactionScope();
        }

        [TearDown]
        public void TearDown() => _scope.Dispose();

        private int CreateTestCart()
        {
            var cart = new Cart { UserID = 1 }; // Припускаємо, що UserID=1 існує
            _cartDal.Insert(cart);
            return _cartDal.GetAll().Last().CartID;
        }

        private int CreateTestProduct()
        {
            var product = new Product { ProductName = "ItemProduct", Price = 5m, Quantity = 1, CategoryID = 1 };
            _productDal.Insert(product);
            return _productDal.GetAll().Last().ProductID;
        }

        [Test]
        public void InsertAndGetById_ShouldWork()
        {
            int cartId = CreateTestCart();
            int productId = CreateTestProduct();

            var item = new CartItem { CartID = cartId, ProductID = productId, Quantity = 2 };
            _dal.Insert(item);

            var inserted = _dal.GetAll().Last();
            Assert.IsNotNull(inserted);
            Assert.AreEqual(2, inserted.Quantity);
        }

        [Test]
        public void Update_ShouldModifyCartItem()
        {
            int cartId = CreateTestCart();
            int productId = CreateTestProduct();

            var item = new CartItem { CartID = cartId, ProductID = productId, Quantity = 1 };
            _dal.Insert(item);
            var inserted = _dal.GetAll().Last();

            inserted.Quantity = 5;
            _dal.Update(inserted);

            var updated = _dal.GetById(inserted.CartItemID);
            Assert.AreEqual(5, updated.Quantity);
        }

        [Test]
        public void Delete_ShouldRemoveCartItem()
        {
            int cartId = CreateTestCart();
            int productId = CreateTestProduct();

            var item = new CartItem { CartID = cartId, ProductID = productId, Quantity = 1 };
            _dal.Insert(item);
            var inserted = _dal.GetAll().Last();

            _dal.Delete(inserted.CartItemID);
            var deleted = _dal.GetById(inserted.CartItemID);
            Assert.IsNull(deleted);
        }
    }
}
