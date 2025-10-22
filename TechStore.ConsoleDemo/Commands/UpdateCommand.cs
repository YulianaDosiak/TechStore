using System;
using TechStore.DAL;
using TechStore.DAL.Concrete;
using TechStore.DAL.Interfaces;
using TechStore.DTO;

namespace TechStore.ConsoleDemo.Commands
{
    public class UpdateCommand : ICommand
    {
        private readonly TechStoreDbContext _context;

        public UpdateCommand(TechStoreDbContext context)
        {
            _context = context;
        }

        public void Execute()
        {
            ICategoryDAL categoryDal = new CategoryDAL(_context);

            Console.Write("Enter category ID to update: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var category = categoryDal.GetById(id);

                if (category != null)
                {
                    Console.Write($"Current name: {category.CategoryName}. Enter new category name: ");
                    string newName = Console.ReadLine();

                    if (!string.IsNullOrWhiteSpace(newName))
                    {
                        category.CategoryName = newName;
                        categoryDal.Update(category);
                        Console.WriteLine("\nCategory updated successfully.\n");
                    }
                    else
                    {
                        Console.WriteLine("New name cannot be empty.");
                    }
                }
                else
                {
                    Console.WriteLine($"\nCategory with ID {id} not found.\n");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID format.");
            }
        }
    }
}