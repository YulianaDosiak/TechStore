using System;
using TechStore.ConsoleDemo.Commands;

namespace TechStore.ConsoleDemo.AppMenu
{
    public class Menu
    {
        public void Start()
        {
            bool running = true;

            while (running)
            {
                var appMenu = new AppMenu();
                appMenu.ShowMainMenu();
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": new GetAllCommand().Execute(); break;
                    case "2": new GetByIdCommand().Execute(); break;
                    case "3": new InsertCommand().Execute(); break;
                    case "4": new UpdateCommand().Execute(); break;
                    case "5": new DeleteByIdCommand().Execute(); break;
                    case "0": running = false; break;
                    default: Console.WriteLine("Invalid choice. Try again."); break;
                }
            }
        }
    }
}
