using System;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using TechStore.BLL.Interfaces;
using TechStore.WPF.Commands;
using TechStore.WPF.Services;

namespace TechStore.WPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly UserSession _userSession;

        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set { _currentViewModel = value; OnPropertyChanged(); }
        }

        public ICommand NavigateHomeCommand { get; }
        public ICommand NavigateCartCommand { get; }
        public ICommand LogoutCommand { get; }

        public MainViewModel(IServiceProvider serviceProvider, UserSession userSession)
        {
            _serviceProvider = serviceProvider;
            _userSession = userSession;

            NavigateHomeCommand = new RelayCommand(_ => NavigateToHome());
            NavigateCartCommand = new RelayCommand(_ => NavigateToCart());
            LogoutCommand = new RelayCommand(_ => Logout());

            NavigateToLogin();
        }

        public void NavigateToLogin()
        {
            CurrentViewModel = new LoginViewModel(
                _serviceProvider.GetRequiredService<IAuthService>(),
                _userSession,
                this
            );
        }

        public void NavigateToHome()
        {
            CurrentViewModel = _serviceProvider.GetRequiredService<HomeViewModel>();
        }

        public void NavigateToCart()
        {
            CurrentViewModel = _serviceProvider.GetRequiredService<CartViewModel>();
        }

        private void Logout()
        {
            _userSession.Clear();
            NavigateToLogin();
        }
    }
}