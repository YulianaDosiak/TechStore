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
    public class CartViewModel : ViewModelBase
    {
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;
        private readonly UserSession _userSession;

        // Використовуємо повну властивість, щоб оновлювати інтерфейс
        private ObservableCollection<CartItems> _cartItems;
        public ObservableCollection<CartItems> CartItems
        {
            get => _cartItems;
            set { _cartItems = value; OnPropertyChanged(); }
        }

        private CartItems _selectedCartItem;
        public CartItems SelectedCartItem
        {
            get => _selectedCartItem;
            set { _selectedCartItem = value; OnPropertyChanged(); }
        }

        public ICommand CreateOrderCommand { get; }
        public ICommand RemoveItemCommand { get; }

        public CartViewModel(ICartService cartService, IOrderService orderService, UserSession userSession)
        {
            _cartService = cartService;
            _orderService = orderService;
            _userSession = userSession;

            CartItems = new ObservableCollection<CartItems>();
            LoadCart(); // Завантажуємо дані при старті

            CreateOrderCommand = new RelayCommand(CreateOrder);
            RemoveItemCommand = new RelayCommand(RemoveItem);
        }

        private void LoadCart()
        {
            if (_userSession.IsLoggedIn)
            {
                // 1. Знаходимо кошик користувача
                var cart = _cartService.GetCartByUserId(_userSession.CurrentUser.UserID);


                var items = _cartService.GetCartItems(cart.CartID);

                CartItems = new ObservableCollection<CartItems>(items);
            }
        }

        private void CreateOrder(object obj)
        {
            if (CartItems != null && CartItems.Any())
            {
                _orderService.CreateOrder(_userSession.CurrentUser.UserID, CartItems.ToList());
                MessageBox.Show("Замовлення успішно створено!");

                foreach (var item in CartItems.ToList())
                {
                    _cartService.RemoveItemFromCart(item.CartItemID);
                }
                CartItems.Clear();
            }
            else
            {
                MessageBox.Show("Кошик порожній!");
            }
        }

        private void RemoveItem(object obj)
        {
            if (SelectedCartItem != null)
            {
                _cartService.RemoveItemFromCart(SelectedCartItem.CartItemID);
                CartItems.Remove(SelectedCartItem);
            }
            else
            {
                MessageBox.Show("Оберіть товар для видалення");
            }
        }
    }
}