using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using TechStore.BLL.Interfaces;
using TechStore.DTO;
using TechStore.WPF.Commands;
using TechStore.WPF.Services;

namespace TechStore.WPF.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private readonly IProductService _productService;
        private readonly ICartService _cartService;
        private readonly UserSession _userSession;
        private readonly ICategoryService _categoryService;

        public ObservableCollection<Product> Products { get; set; }
        public ObservableCollection<Category> Categories { get; set; }

        private Product _selectedProduct;
        public Product SelectedProduct
        {
            get => _selectedProduct;
            set { _selectedProduct = value; OnPropertyChanged(); }
        }

        public ICommand AddToCartCommand { get; }

        public HomeViewModel(IProductService productService, ICategoryService categoryService, ICartService cartService, UserSession userSession)
        {
            _productService = productService;
            _categoryService = categoryService;
            _cartService = cartService;
            _userSession = userSession;

            Products = new ObservableCollection<Product>(_productService.GetAllProducts());
            Categories = new ObservableCollection<Category>(_categoryService.GetAllCategories());

            AddToCartCommand = new RelayCommand(AddToCart);
        }

        private void AddToCart(object obj)
        {
            if (SelectedProduct != null && _userSession.IsLoggedIn)
            {
                _cartService.AddItemToCart(_userSession.CurrentUser.UserID, SelectedProduct.ProductID, 1);
                MessageBox.Show("Product added to cart");
            }
            else
            {
                MessageBox.Show("Please select a product or log in");
            }
        }
    }
}