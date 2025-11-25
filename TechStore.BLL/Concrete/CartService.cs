using System.Collections.Generic;
using System.Linq;
using TechStore.BLL.Interfaces;
using TechStore.DAL.Interfaces;
using TechStore.DTO;

namespace TechStore.BLL.Concrete
{
    public class CartService : ICartService
    {
        private readonly ICartDAL _cartDal;
        private readonly ICartItemsDAL _cartItemsDal;
        private readonly IProductDAL _productDal;

        public CartService(ICartDAL cartDal, ICartItemsDAL cartItemsDal, IProductDAL productDal)
        {
            _cartDal = cartDal;
            _cartItemsDal = cartItemsDal;
            _productDal = productDal;
        }

        public Cart GetCartByUserId(int userId)
        {
            var carts = _cartDal.GetAll();
            var userCart = carts.FirstOrDefault(c => c.UserID == userId);

            if (userCart == null)
            {
                userCart = _cartDal.Create(new Cart { UserID = userId });
            }
            return userCart;
        }

        public List<CartItems> GetCartItems(int cartId)
        {
           
            var items = _cartItemsDal.GetAll().Where(item => item.CartID == cartId).ToList();

          
            foreach (var item in items)
            {
                var product = _productDal.GetById(item.ProductID);
                if (product != null)
                {
                    item.ProductName = product.ProductName;
                    item.Price = product.Price;
                }
            }

            return items;
        }

        public void AddItemToCart(int userId, int productId, int quantity)
        {
            var cart = GetCartByUserId(userId);
            var product = _productDal.GetById(productId);

            if (product == null) return;

            var cartItem = new CartItems
            {
                CartID = cart.CartID,
                ProductID = productId,
                Quantity = quantity
            };

            _cartItemsDal.Create(cartItem);
        }

        public void RemoveItemFromCart(int cartItemId)
        {
            _cartItemsDal.Delete(cartItemId);
        }
    }
}