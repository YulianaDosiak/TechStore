using System.ComponentModel.DataAnnotations;

namespace TechStore.MVC.Models
{
    public class CategoryViewModel
    {
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Назва категорії є обов'язковою")]
        [StringLength(50, ErrorMessage = "Довжина назви не може перевищувати 50 символів")]
        public string CategoryName { get; set; }
    }
}