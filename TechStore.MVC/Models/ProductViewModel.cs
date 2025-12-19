using System.ComponentModel.DataAnnotations;
using TechStore.MVC.App.Validation;

namespace TechStore.MVC.Models
{
    public class ProductViewModel
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }

        [Required]
        [PriceRange(1, 1000000)]
        public decimal Price { get; set; }
    }
}