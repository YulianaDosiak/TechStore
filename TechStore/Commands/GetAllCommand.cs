using System;
using System.Linq; 
using TechStore.DAL;
using TechStore.DAL.Concrete;
using TechStore.DAL.Interfaces;

namespace TechStore.ConsoleDemo.Commands
{
    public class GetAllCommand : ICommand
    {
        private readonly TechStoreDbContext _context;

        public GetAllCommand(TechStoreDbContext context)
        {
            _context = context;
        }

        public void Execute()
        {
            ICategoryDAL categoryDal = new CategoryDAL(_context);

            try
            {
                var allCategories = categoryDal.GetAll();

                Console.WriteLine("\n===== All Categories (Demo) =====");
                if (!allCategories.Any())
                {
                    Console.WriteLine("No categories found.");
                }
                else
                {
                    foreach (var cat in allCategories)
                    {
                        Console.WriteLine($"Id: {cat.CategoryID}, Name: {cat.CategoryName}");
                    }
                }
                Console.WriteLine("=================================\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving categories: {ex.Message}");
            }
        }
    }
}