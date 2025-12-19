using System.Collections.Generic;

namespace TechStore.MVC.Models
{
    public class CategoryIndexViewModel
    {
        public IEnumerable<CategoryViewModel> Categories { get; set; }
        public IEnumerable<ProductViewModel> Products { get; set; }
        public int? SelectedCategoryId { get; set; }
    }
}