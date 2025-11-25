using System.Collections.Generic;
using TechStore.DTO;

namespace TechStore.BLL.Interfaces
{
    public interface ICartService
    {
        Cart GetCartByUserId(int userId);
        List<CartItems> GetCartItems(int cartId);

        void AddItemToCart(int userId, int productId, int quantity);
        void RemoveItemFromCart(int cartItemId);
    }
}