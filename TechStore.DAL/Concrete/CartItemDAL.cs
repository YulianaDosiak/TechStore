using System.Collections.Generic;
using System.Linq;
using TechStore.DTO;
using TechStore.DAL.Interfaces;

namespace TechStore.DAL
{
    public class CartItemDAL : GenericDAL<CartItem>, ICartItemDAL
    {
        public List<CartItem> GetByCartId(int cartId)
        {
            return _data.Where(ci => ci.CartId == cartId).ToList();
        }

        public bool RemoveFromCart(int cartId, int productId)
        {
            var item = _data.FirstOrDefault(ci => ci.CartId == cartId && ci.ProductId == productId);
            if (item != null)
            {
                _data.Remove(item);
                return true;
            }
            return false;
        }
    }
}
