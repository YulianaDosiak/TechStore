using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using TechStore.AppMenu;
using TechStore.DALEF.AutoMapper;

namespace TechStore
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var configExpression = new MapperConfigurationExpression();

            configExpression.AddProfile<CategoryMap>();
            configExpression.AddProfile<ProductMap>();
            configExpression.AddProfile<UserMap>();
            configExpression.AddProfile<OrderMap>();
            configExpression.AddProfile<OrderItemMap>();
            configExpression.AddProfile<CartMap>();
            configExpression.AddProfile<CartItemsMap>();

            var loggerFactory = NullLoggerFactory.Instance;
            var mapperConfig = new MapperConfiguration(configExpression, loggerFactory);
            IMapper mapper = mapperConfig.CreateMapper();

            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            string connectionString = config.GetConnectionString("DefaultConnection");

            new AppMenuService(connectionString, mapper).Show();
        }
    }
}