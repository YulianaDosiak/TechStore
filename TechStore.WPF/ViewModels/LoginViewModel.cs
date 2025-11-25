using System.Windows.Input;
using TechStore.BLL.Interfaces;
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
        private string _password;
        private string _errorMessage;

        public string Username
        {
            get => _username;
            set { _username = value; OnPropertyChanged(); }
        }

        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(); }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set { _errorMessage = value; OnPropertyChanged(); }
        }

        public ICommand LoginCommand { get; }

        public LoginViewModel(IAuthService authService, UserSession userSession, MainViewModel mainViewModel)
        {
            _authService = authService;
            _userSession = userSession;
            _mainViewModel = mainViewModel;
            LoginCommand = new RelayCommand(Login);
        }

        private void Login(object obj)
        {
            var user = _authService.Login(Username, Password);
            if (user != null)
            {
                _userSession.SetUser(user);
                _mainViewModel.NavigateToHome();
            }
            else
            {
                ErrorMessage = "Invalid credentials";
            }
        }
    }
}