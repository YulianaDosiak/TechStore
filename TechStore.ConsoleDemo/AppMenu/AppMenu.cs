using System;

namespace TechStore.ConsoleDemo.AppMenu
{
    public class AppMenu
    {
        public void ShowMainMenu()
        {
            Console.WriteLine("===== TechStore Menu =====");
            Console.WriteLine("1. Get all categories");
            Console.WriteLine("2. Get category by ID");
            Console.WriteLine("3. Insert category");
            Console.WriteLine("4. Update category");
            Console.WriteLine("5. Delete category");
            Console.WriteLine("0. Exit");
            Console.WriteLine("==========================");
        }
    }
}
