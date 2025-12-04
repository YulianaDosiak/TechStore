using System.Net.Mail;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TechStore.BLL.Interfaces;
using TechStore.DTO;
using TechStore.WPF.Commands;
using TechStore.WPF.Services;

namespace TechStore.WPF.ViewModels
{
    public class RegisterViewModel : ViewModelBase
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

        private string _email;
        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChanged(); }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set { _errorMessage = value; OnPropertyChanged(); }
        }

        public ICommand RegisterCommand { get; }
        public ICommand BackToLoginCommand { get; }

        public RegisterViewModel(IAuthService authService, UserSession userSession, MainViewModel mainViewModel)
        {
            _authService = authService;
            _userSession = userSession;
            _mainViewModel = mainViewModel;

            RegisterCommand = new RelayCommand(Register);
            BackToLoginCommand = new RelayCommand(_ => _mainViewModel.NavigateToLogin());
        }

        protected override string Validate(string columnName)
        {
            string error = null;
            switch (columnName)
            {
                case nameof(Username):
                    if (string.IsNullOrWhiteSpace(Username))
                        error = "Username is required";
                    else if (Username.Length < 3)
                        error = "Username must be at least 3 chars";
                    break;

                case nameof(Email):
                    if (string.IsNullOrWhiteSpace(Email))
                        error = "Email is required";
                    else if (!IsValidEmail(Email))
                        error = "Invalid email format";
                    break;
            }
            return error;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void Register(object parameter)
        {
            var passwordBox = parameter as PasswordBox;
            var password = passwordBox?.Password;

            if (string.IsNullOrWhiteSpace(Username) ||
                string.IsNullOrWhiteSpace(Email) ||
                string.IsNullOrWhiteSpace(password))
            {
                ErrorMessage = "Please fill in all fields!";
                return;
            }

            if (!IsValidEmail(Email))
            {
                ErrorMessage = "Please enter a valid email address (e.g., name@mail.com)";
                return;
            }

            var newUser = new User
            {
                Username = Username,
                Email = Email,
                Password = password
            };

            bool success = _authService.Register(newUser);

            if (success)
            {
                MessageBox.Show("Account created successfully! Welcome!");

                var user = _authService.Login(Username, password);
                if (user != null)
                {
                    _userSession.SetUser(user);
                    _mainViewModel.NavigateToHome();
                }
                else
                {
                    _mainViewModel.NavigateToLogin();
                }
            }
            else
            {
                ErrorMessage = $"User '{Username}' already exists. Try another name.";
            }
        }
    }
}