using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TechStore.BLL.Interfaces;
using TechStore.DTO; 
using TechStore.WPF.Commands;
using TechStore.WPF.Services;

namespace TechStore.WPF.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IAuthService _authService;
        private readonly UserSession _userSession;
        private readonly MainViewModel _mainViewModel;

        private string _username;
        public string Username
        {
            get => _username;
            set { _username = value; OnPropertyChanged(); }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set { _errorMessage = value; OnPropertyChanged(); }
        }

        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; } 

        public LoginViewModel(IAuthService authService, UserSession userSession, MainViewModel mainViewModel)
        {
            _authService = authService;
            _userSession = userSession;
            _mainViewModel = mainViewModel;

            LoginCommand = new RelayCommand(Login);
            RegisterCommand = new RelayCommand(Register); 
        }

        private void Login(object parameter)
        {
            var passwordBox = parameter as PasswordBox;
            var password = passwordBox?.Password;

            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(password))
            {
                ErrorMessage = "Please enter username and password";
                return;
            }

            var user = _authService.Login(Username, password);
            if (user != null)
            {
                _userSession.SetUser(user);
                _mainViewModel.NavigateToHome();
            }
            else
            {
                ErrorMessage = "Invalid login or password";
            }
        }

        private void Register(object parameter)
        {
            _mainViewModel.NavigateToRegister();
        }
    }
}