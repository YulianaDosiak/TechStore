using System.ComponentModel.DataAnnotations;

namespace TechStore.MVC.Models
{
    public class CategoryViewModel
    {
        public int CategoryID { get; set; }

        [Display(Name = "Назва категорії")]
        [Required(ErrorMessage = "Будь ласка, введіть назву категорії")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Назва повинна містити від 3 до 50 символів")]
        public string CategoryName { get; set; }
    }
}