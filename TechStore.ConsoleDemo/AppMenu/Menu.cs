using System;
using TechStore.ConsoleDemo.Commands;
using TechStore.DAL;

namespace TechStore.ConsoleDemo.AppMenu
{
    public class Menu
    {
        private readonly TechStoreDbContext _context;

        public Menu(TechStoreDbContext context)
        {
            _context = context;
        }

        public void Start()
        {
            bool running = true;

            while (running)
            {
                new AppMenu().ShowMainMenu();
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": new GetAllCommand(_context).Execute(); break;
                    case "2": new GetByIdCommand(_context).Execute(); break;
                    case "3": new InsertCommand(_context).Execute(); break;
                    case "4": new UpdateCommand(_context).Execute(); break;
                    case "5": new DeleteByIdCommand(_context).Execute(); break;
                    case "0": running = false; break;
                    default: Console.WriteLine("Invalid choice. Try again."); break;
                }
            }
        }
    }
}