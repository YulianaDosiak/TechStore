using System;
using System.Windows.Input;
using TechStore.DAL;
using TechStore.DAL.Concrete;
using TechStore.DAL.Interfaces;
using TechStore.DTO;

namespace TechStore.ConsoleDemo.Commands
{
    public class InsertCommand : ICommand
    {
        public void Execute()
        {
            ICategoryDAL categoryDal = new CategoryDAL();

            Console.Write("Enter category name: ");
            string name = Console.ReadLine()!;

            var category = new Category { CategoryName = name };
            categoryDal.Insert(category);

            Console.WriteLine("Category inserted successfully.");
        }
    }
}
