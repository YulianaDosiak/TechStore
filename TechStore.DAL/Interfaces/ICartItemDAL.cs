using TechStore.DTO;
using System.Collections.Generic;

namespace TechStore.DAL.Interfaces
{
    public interface ICartItemDAL : IGenericDAL<CartItem>
    {
        List<CartItem> GetByCartId(int cartId);
        bool RemoveFromCart(int cartId, int productId);
    }
}
