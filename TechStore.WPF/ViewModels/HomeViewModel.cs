using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        private readonly ICategoryService _categoryService;
        private readonly UserSession _userSession;

        private List<Product> _allProducts;

        private ObservableCollection<Product> _products;
        public ObservableCollection<Product> Products
        {
            get => _products;
            set { _products = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Category> Categories { get; set; }

        private Product _selectedProduct;
        public Product SelectedProduct
        {
            get => _selectedProduct;
            set { _selectedProduct = value; OnPropertyChanged(); }
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                ApplyFilters();
            }
        }

        private Category _selectedCategory;
        public Category SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged();
                ApplyFilters(); 
            }
        }

        private string _selectedSort;
        public string SelectedSort
        {
            get => _selectedSort;
            set
            {
                _selectedSort = value;
                OnPropertyChanged();
                ApplyFilters();
            }
        }
        public ObservableCollection<string> SortOptions { get; } = new ObservableCollection<string>
        {
            "Name (A-Z)",
            "Price (Low to High)",
            "Price (High to Low)"
        };

        public ICommand AddToCartCommand { get; }
        public ICommand ResetFilterCommand { get; }

        public HomeViewModel(IProductService productService, ICategoryService categoryService, ICartService cartService, UserSession userSession)
        {
            _productService = productService;
            _categoryService = categoryService;
            _cartService = cartService;
            _userSession = userSession;

            _allProducts = _productService.GetAllProducts();
            Categories = new ObservableCollection<Category>(_categoryService.GetAllCategories());

            Products = new ObservableCollection<Product>(_allProducts);

            AddToCartCommand = new RelayCommand(AddToCart);
            ResetFilterCommand = new RelayCommand(ResetFilters);
        }

        private void ApplyFilters()
        {
            var query = _allProducts.AsEnumerable();

            // 1. Filter by Search Text
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                query = query.Where(p => p.ProductName.ToLower().Contains(SearchText.ToLower()));
            }

            // 2. Filter by Category
            if (SelectedCategory != null)
            {
                query = query.Where(p => p.CategoryID == SelectedCategory.CategoryID);
            }

            switch (SelectedSort)
            {
                case "Name (A-Z)":
                    query = query.OrderBy(p => p.ProductName);
                    break;
                case "Price (Low to High)":
                    query = query.OrderBy(p => p.Price);
                    break;
                case "Price (High to Low)":
                    query = query.OrderByDescending(p => p.Price);
                    break;
            }

            Products = new ObservableCollection<Product>(query.ToList());
        }

        private void ResetFilters(object obj)
        {
            SearchText = string.Empty;
            SelectedCategory = null;
            SelectedSort = null;
            ApplyFilters();
        }

        private void AddToCart(object obj)
        {
            if (SelectedProduct != null && _userSession.IsLoggedIn)
            {
                _cartService.AddItemToCart(_userSession.CurrentUser.UserID, SelectedProduct.ProductID, 1);
                MessageBox.Show($"{SelectedProduct.ProductName} added to cart!");
            }
            else
            {
                MessageBox.Show("Please select a product or log in.");
            }
        }
    }
}