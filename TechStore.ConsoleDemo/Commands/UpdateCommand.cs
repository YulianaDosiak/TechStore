using System;
using System.Windows.Input;
using TechStore.DAL;
using TechStore.DAL.Concrete;
using TechStore.DAL.Interfaces;
using TechStore.DTO;

namespace TechStore.ConsoleDemo.Commands
{
    public class UpdateCommand : ICommand
    {
        public void Execute()
        {
            ICategoryDAL categoryDal = new CategoryDAL();

            Console.Write("Enter category ID to update: ");
            int id = int.Parse(Console.ReadLine()!);
            var category = categoryDal.GetById(id);

            if (category != null)
            {
                Console.Write("Enter new category name: ");
                category.CategoryName = Console.ReadLine()!;
                categoryDal.Update(category);
                Console.WriteLine("Category updated successfully.");
            }
            else
            {
                Console.WriteLine("Category not found.");
            }
        }
    }
}
