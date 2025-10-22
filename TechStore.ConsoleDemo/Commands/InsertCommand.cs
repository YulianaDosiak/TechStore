using System;
using TechStore.DAL;
using TechStore.DAL.Concrete;
using TechStore.DAL.Interfaces;
using TechStore.DTO;

namespace TechStore.ConsoleDemo.Commands
{
    public class InsertCommand : ICommand
    {
        private readonly TechStoreDbContext _context;

        public InsertCommand(TechStoreDbContext context)
        {
            _context = context;
        }

        public void Execute()
        {
            ICategoryDAL categoryDal = new CategoryDAL(_context);

            Console.Write("Enter new category name: ");
            string name = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(name))
            {
                try
                {
                    var category = new Category { CategoryName = name };
                    categoryDal.Insert(category);
                    Console.WriteLine("\nCategory inserted successfully.\n");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error inserting category: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Category name cannot be empty.");
            }
        }
    }
}