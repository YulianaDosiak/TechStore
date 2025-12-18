using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TechStore.BLL.Interfaces;
using TechStore.DAL.Interfaces; // Для DAL
using TechStore.DTO;
using TechStore.MVC.Models;

namespace TechStore.MVC.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly ICartItemsDAL _cartItemsDal; // Прямий доступ для CRUD
        private readonly ICartDAL _cartDal;
        private readonly IProductService _productService;

        public CartController(ICartService cartService, ICartItemsDAL cartItemsDal, ICartDAL cartDal, IProductService productService)
        {
            _cartService = cartService;
            _cartItemsDal = cartItemsDal;
            _cartDal = cartDal;
            _productService = productService;
        }

        private int GetUserId() => int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

        // READ (CRUD)
        public IActionResult Index()
        {
            var userId = GetUserId();
            var cart = _cartService.GetCartByUserId(userId);
            var model = new List<CartItemViewModel>();

            if (cart != null)
            {
                var items = _cartItemsDal.GetAll().Where(x => x.CartID == cart.CartID).ToList();
                var products = _productService.GetAllProducts();

                foreach (var item in items)
                {
                    var p = products.FirstOrDefault(x => x.ProductID == item.ProductID);
                    if (p != null)
                    {
                        model.Add(new CartItemViewModel
                        {
                            CartItemId = item.CartItemID,
                            ProductName = p.ProductName,
                            Price = p.Price,
                            Quantity = item.Quantity
                        });
                    }
                }
            }
            return View(model);
        }

        // CREATE (CRUD)
        public IActionResult Add(int productId)
        {
            var userId = GetUserId();
            var cart = _cartService.GetCartByUserId(userId);

            if (cart == null)
            {
                _cartDal.Create(new Cart { UserID = userId });
                cart = _cartService.GetCartByUserId(userId);
            }

            _cartItemsDal.Create(new CartItems { CartID = cart.CartID, ProductID = productId, Quantity = 1 });
            return RedirectToAction("Index", "Product");
        }

        // DELETE (CRUD)
        // БУЛО: public IActionResult Remove(int itemId)
        // СТАЛО:
        public IActionResult Remove(int id)
        {
            try
            {
                // Перевіряємо, чи ID прийшов
                if (id == 0)
                {
                    TempData["Error"] = "Помилка: ID товару дорівнює 0";
                    return RedirectToAction("Index");
                }

                _cartItemsDal.Delete(id);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Не вдалося видалити: " + ex.Message;
            }
            return RedirectToAction("Index");
        }
    }
}