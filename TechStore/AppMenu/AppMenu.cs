using System;

namespace TechStore.ConsoleDemo.AppMenu
{
    public class AppMenu
    {
        public void ShowMainMenu()
        {
            Console.WriteLine("===== TechStore DAL Demo Menu =====");
            Console.WriteLine("1. Get all categories (R)");
            Console.WriteLine("2. Get category by ID (R)");
            Console.WriteLine("3. Insert new category (C)");
            Console.WriteLine("4. Update category name (U)");
            Console.WriteLine("5. Delete category by ID (D)");
            Console.WriteLine("0. Exit");
            Console.WriteLine("=================================");
        }
    }
}