using System;
using System.Windows.Input;
using TechStore.DAL;
using TechStore.DAL.Concrete;
using TechStore.DAL.Interfaces;

namespace TechStore.ConsoleDemo.Commands
{
    public class DeleteByIdCommand : ICommand
    {
        public void Execute()
        {
            ICategoryDAL categoryDal = new CategoryDAL();

            Console.Write("Enter category ID to delete: ");
            int id = int.Parse(Console.ReadLine()!);
            categoryDal.Delete(id);

            Console.WriteLine("Category deleted successfully.");
        }
    }
}
