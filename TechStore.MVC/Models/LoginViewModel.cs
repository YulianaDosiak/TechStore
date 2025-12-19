using System.ComponentModel.DataAnnotations;

namespace TechStore.MVC.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Введіть логін")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Введіть пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}