using System;
using TechStore.DAL;
using TechStore.DAL.Concrete;
using TechStore.DAL.Interfaces;
using TechStore.DTO;

namespace TechStore.ConsoleDemo.Commands
{
    public class GetByIdCommand : ICommand
    {
        private readonly TechStoreDbContext _context;

        public GetByIdCommand(TechStoreDbContext context)
        {
            _context = context;
        }

        public void Execute()
        {
            ICategoryDAL categoryDal = new CategoryDAL(_context);

            Console.Write("Enter category ID to retrieve: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var category = categoryDal.GetById(id);

                if (category != null)
                {
                    Console.WriteLine($"\nCategory found: Id: {category.CategoryID}, Name: {category.CategoryName}\n");
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