using System;
using TechStore.DAL;
using TechStore.DAL.Concrete;
using TechStore.DAL.Interfaces;

namespace TechStore.ConsoleDemo.Commands
{
    public class DeleteByIdCommand : ICommand
    {
        private readonly TechStoreDbContext _context;

        public DeleteByIdCommand(TechStoreDbContext context)
        {
            _context = context;
        }

        public void Execute()
        {
            ICategoryDAL categoryDal = new CategoryDAL(_context);

            Console.Write("Enter category ID to delete: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                try
                {
                    var categoryToDelete = categoryDal.GetById(id);

                    if (categoryToDelete != null)
                    {
                        categoryDal.Delete(id);
                        Console.WriteLine($"\nCategory ID {id} deleted successfully.\n");
                    }
                    else
                    {
                        Console.WriteLine($"\nCategory with ID {id} not found. Deletion failed.\n");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error deleting category: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID format.");
            }
        }
    }
}