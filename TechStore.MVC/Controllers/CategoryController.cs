using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TechStore.BLL.Interfaces;
using TechStore.DTO;
using TechStore.MVC.Models;

namespace TechStore.MVC.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(
            ICategoryService categoryService,
            IProductService productService,
            IMapper mapper,
            ILogger<CategoryController> logger)
        {
            _categoryService = categoryService;
            _productService = productService;
            _mapper = mapper;
            _logger = logger;
        }

        public IActionResult Index(int? id)
        {
            var viewModel = new CategoryIndexViewModel();

            var categoryDtos = _categoryService.GetAllCategories();
            viewModel.Categories = _mapper.Map<IEnumerable<CategoryViewModel>>(categoryDtos);

            if (id.HasValue)
            {
                viewModel.SelectedCategoryId = id.Value;

                var allProducts = _productService.GetAllProducts();

                var filteredProducts = allProducts
                                        .Where(p => p.CategoryID == id.Value)
                                        .ToList();

                viewModel.Products = _mapper.Map<IEnumerable<ProductViewModel>>(filteredProducts);

                if (!filteredProducts.Any() && allProducts.Any())
                {
                    foreach (var p in allProducts)
                    {
                        Console.WriteLine($"Product: {p.Productname}, CatID: {p.CategoryID}");
                    }
                }
            }

            return View(viewModel);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dto = _mapper.Map<Category>(model);
                _categoryService.AddCategory(dto);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var dto = _categoryService.GetCategoryById(id);
            if (dto == null) return NotFound();
            return View(_mapper.Map<CategoryViewModel>(dto));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dto = _mapper.Map<Category>(model);
                _categoryService.UpdateCategory(dto);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        public IActionResult Delete(int id)
        {
            var allProducts = _productService.GetAllProducts();

            bool hasProducts = allProducts.Any(p => p.CategoryID == id);

            if (hasProducts)
            {
                TempData["Error"] = "Увага! Неможливо видалити категорію, бо в ній є товари. Спочатку видаліть товари.";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _categoryService.DeleteCategory(id);
                TempData["Success"] = "Категорію успішно видалено.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Помилка бази даних: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}