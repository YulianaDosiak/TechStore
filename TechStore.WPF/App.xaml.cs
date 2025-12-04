using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using TechStore.BLL.Concrete;
using TechStore.BLL.Interfaces;
using TechStore.DAL.Interfaces;
using TechStore.DALEF.Concrete;
using TechStore.DALEF.Data;
using TechStore.WPF.Services;
using TechStore.WPF.ViewModels;
using AutoMapper;

namespace TechStore.WPF
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        { 
            string connectionString = "Server=localhost;Database=TechStoreDB;Integrated Security=True;TrustServerCertificate=True;";

            services.AddAutoMapper(typeof(UserDALEF).Assembly);

            services.AddScoped<IUserDAL>(provider =>
                new UserDALEF(connectionString, provider.GetRequiredService<IMapper>()));

            services.AddScoped<IProductDAL>(provider =>
                new ProductDALEF(connectionString, provider.GetRequiredService<IMapper>()));

            services.AddScoped<ICategoryDAL>(provider =>
                new CategoryDALEF(connectionString, provider.GetRequiredService<IMapper>()));

            services.AddScoped<ICartDAL>(provider =>
                new CartDALEF(connectionString, provider.GetRequiredService<IMapper>()));

            services.AddScoped<ICartItemsDAL>(provider =>
                new CartItemsDALEF(connectionString, provider.GetRequiredService<IMapper>()));

            services.AddScoped<IOrderDAL>(provider =>
                new OrderDALEF(connectionString, provider.GetRequiredService<IMapper>()));

            services.AddScoped<IOrderItemDAL>(provider =>
                new OrderItemDALEF(connectionString, provider.GetRequiredService<IMapper>()));

            // BLL
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IOrderService, OrderService>();


            // WPF
            services.AddSingleton<UserSession>();
            services.AddSingleton<MainViewModel>();
            services.AddTransient<LoginViewModel>();
            services.AddTransient<HomeViewModel>();
            services.AddTransient<CartViewModel>();
            services.AddTransient<RegisterViewModel>();
            services.AddTransient<MainWindow>();
        }
    }
}