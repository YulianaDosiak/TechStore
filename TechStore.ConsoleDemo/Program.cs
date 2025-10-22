using System;
using TechStore.ConsoleDemo.AppMenu;
using TechStore.DAL;

namespace TechStore.ConsoleDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            const string connectionString = "Data Source=localhost;Initial Catalog=TechStore;Integrated Security=True;TrustServerCertificate=True";

            using (var context = new TechStoreDbContext(connectionString))
            {
                var menu = new Menu(context);
                menu.Start();
            }
        }
    }
}

