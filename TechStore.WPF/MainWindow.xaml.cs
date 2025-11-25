using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using TechStore.WPF.ViewModels;

namespace TechStore.WPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = App.ServiceProvider.GetRequiredService<MainViewModel>();
        }
    }
}