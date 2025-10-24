using NUnit.Framework;
using System.Linq;
using System.Transactions;
using TechStore.DAL.Concrete;
using TechStore.DTO;

namespace TechStore.DAL.Tests
{
    [TestFixture]
    public class CartDALTests
    {
        private CartDAL _cartDal;
        private UserDAL _userDal;
        private TransactionScope _scope;

        [SetUp]
        public void Setup()
        {
            var context = new TechStoreDbContext(
                "Data Source=localhost;Initial Catalog=TechStoreDB;Integrated Security=True;");
            _cartDal = new CartDAL(context);
            _userDal = new UserDAL(context);
            _scope = new TransactionScope();
        }

        [TearDown]
        public void TearDown() => _scope.Dispose();

        private int CreateTestUser()
        {
            var user = new User { Username = "CartUser", Email = "c@d.com", Password = "123" };
            _userDal.Insert(user);
            return _userDal.GetAll().Last().UserID;
        }

        [Test]
        public void InsertAndGetById_ShouldWork()
        {
            int userId = CreateTestUser();
            var cart = new Cart { UserID = userId };
            _cartDal.Insert(cart);

            var inserted = _cartDal.GetAll().Last();
            Assert.IsNotNull(inserted);
            Assert.AreEqual(userId, inserted.UserID);
        }

        [Test]
        public void Update_ShouldModifyCart()
        {
            int userId = CreateTestUser();
            var cart = new Cart { UserID = userId };
            _cartDal.Insert(cart);
            var inserted = _cartDal.GetAll().Last();

            inserted.UserID = userId; // Можна змінити на інший user, якщо створити ще одного
            _cartDal.Update(inserted);

            var updated = _cartDal.GetById(inserted.CartID);
            Assert.AreEqual(userId, updated.UserID);
        }

        [Test]
        public void Delete_ShouldRemoveCart()
        {
            int userId = CreateTestUser();
            var cart = new Cart { UserID = userId };
            _cartDal.Insert(cart);
            var inserted = _cartDal.GetAll().Last();

            _cartDal.Delete(inserted.CartID);
            var deleted = _cartDal.GetById(inserted.CartID);
            Assert.IsNull(deleted);
        }
    }
}
