using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechStore.BLL.Interfaces; // Використовуємо BLL, а не DAL
using TechStore.DTO;
using TechStore.MVC.Models;

namespace TechStore.MVC.Controllers
{
    [Authorize] // Авторизація
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService; // Тільки сервіс!
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryController> _logger;

        // Інверсія залежностей (DI) через конструктор
        public CategoryController(ICategoryService categoryService, IMapper mapper, ILogger<CategoryController> logger)
        {
            _categoryService = categoryService;
            _mapper = mapper;
            _logger = logger;
        }

        // READ
        public IActionResult Index()
        {
            var dtos = _categoryService.GetAllCategories();
            // Використання AutoMapper
            var viewModels = _mapper.Map<IEnumerable<CategoryViewModel>>(dtos);
            return View(viewModels);
        }

        // CREATE (GET)
        public IActionResult Create() => View();

        // CREATE (POST)
        [HttpPost]
        public IActionResult Create(CategoryViewModel model)
        {
            // Валідація даних
            if (ModelState.IsValid)
            {
                var dto = _mapper.Map<Category>(model);
                _categoryService.CreateCategory(dto); // Метод BLL
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // EDIT (GET)
        public IActionResult Edit(int id)
        {
            var dto = _categoryService.GetAllCategories().FirstOrDefault(c => c.CategoryID == id); // Краще мати метод GetById у сервісі
            if (dto == null) return NotFound();

            var viewModel = _mapper.Map<CategoryViewModel>(dto);
            return View(viewModel);
        }

        // EDIT (POST)
        [HttpPost]
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

        // DELETE
        // Логування спроби без прав (якщо користувач не адмін, наприклад)
        public IActionResult Delete(int id)
        {
            // Приклад логування
            _logger.LogInformation($"User {User.Identity.Name} is deleting category {id}");

            _categoryService.DeleteCategory(id);
            return RedirectToAction(nameof(Index));
        }
    }
}