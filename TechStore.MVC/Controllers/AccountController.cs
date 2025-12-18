using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TechStore.BLL.Interfaces;
using TechStore.MVC.Models; // Для LoginViewModel

namespace TechStore.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IAuthService authService, ILogger<AccountController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid) // Валідація
            {
                var user = _authService.Login(model.Username, model.Password);
                if (user != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
                        new Claim(ClaimTypes.Role, "Buyer") // Роль Покупець
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme)));

                    return RedirectToAction("Index", "Product");
                }
                _logger.LogWarning($"Failed login attempt for {model.Username}");
            }
            ViewBag.Error = "Невірний логін або пароль";
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        // Логування спроби без прав
        public IActionResult AccessDenied()
        {
            _logger.LogWarning($"Unauthorized access attempt by {User.Identity.Name}");
            return View();
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View(); // Саме тут програма шукає файл .cshtml
        }

        [HttpPost]
        public IActionResult ForgotPassword(string email)
        {
            ViewBag.Message = "Інструкції відправлено (імітація).";
            return View();
        }
    }
}