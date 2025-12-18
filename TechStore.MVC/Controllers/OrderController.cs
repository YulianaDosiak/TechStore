using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using TechStore.BLL.Interfaces;
using TechStore.DAL.Interfaces;
using TechStore.DTO;

namespace TechStore.MVC.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderDAL _orderDal;
        private readonly ICartService _cartService;
        private readonly ICartItemsDAL _cartItemsDal;
        private readonly IProductService _productService; // Щоб отримати ціни

        public OrderController(IOrderDAL orderDal, ICartService cartService, ICartItemsDAL cartItemsDal, IProductService productService)
        {
            _orderDal = orderDal;
            _cartService = cartService;
            _cartItemsDal = cartItemsDal;
            _productService = productService;
        }

        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                return userId;
            }
            return 0;
        }

        // GET: /Order/Checkout
        // Показує сторінку підтвердження
        [HttpGet]
        public IActionResult Checkout()
        {
            return View();
        }

        // POST: /Order/Create
        // Створює замовлення з кошика
        [HttpPost]
        public IActionResult Create(string address)
        {
            var userId = GetCurrentUserId();
            var cart = _cartService.GetCartByUserId(userId);

            if (cart == null)
            {
                return RedirectToAction("Index", "Cart");
            }

            // Отримуємо товари кошика
            var cartItems = _cartItemsDal.GetAll().Where(i => i.CartID == cart.CartID).ToList();
            if (!cartItems.Any())
            {
                TempData["Error"] = "Кошик порожній";
                return RedirectToAction("Index", "Cart");
            }

            try
            {
                // 1. Створюємо замовлення
                var newOrder = new Order
                {
                    UserID = userId,
                    OrderDate = DateTime.Now,
                    TotalAmount = 0, // Порахуємо нижче
                    // Якщо в Order є поле Address, додайте його: Address = address 
                };

                // Рахуємо суму (якщо потрібно)
                var products = _productService.GetAllProducts();
                foreach (var item in cartItems)
                {
                    var product = products.FirstOrDefault(p => p.ProductID == item.ProductID);
                    if (product != null)
                    {
                        newOrder.TotalAmount += product.Price * item.Quantity;
                    }
                }

                // Зберігаємо замовлення
                // УВАГА: Перевірте метод створення в IOrderDAL (Create або Insert)
                _orderDal.Create(newOrder);

                // 2. Очищаємо кошик (видаляємо товари)
                foreach (var item in cartItems)
                {
                    _cartItemsDal.Delete(item.CartItemID);
                }

                return View("OrderComplete");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Помилка замовлення: " + ex.Message;
                return RedirectToAction("Index", "Cart");
            }
        }
    }
}