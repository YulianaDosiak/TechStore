using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechStore.BLL.Interfaces;

namespace TechStore.MVC.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        // Сторінка категорій
        public IActionResult Index()
        {
            return View(_categoryService.GetAllCategories());
        }

        // Товари: Фільтр, Пошук, Сортування
        public IActionResult List(int categoryId, string sortOrder, string searchString)
        {
            var products = _productService.GetAllProducts().Where(p => p.CategoryID == categoryId);

            // Пошук
            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.ProductName.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            }

            // Сортування
            ViewBag.SortParam = String.IsNullOrEmpty(sortOrder) ? "price_desc" : "";

            if (sortOrder == "price_desc")
                products = products.OrderByDescending(p => p.Price);
            else
                products = products.OrderBy(p => p.Price);

            ViewBag.CategoryId = categoryId;
            return View(products.ToList());
        }
    }
}