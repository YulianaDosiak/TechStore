using System;
using System.Windows.Input;
using TechStore.DAL;
using TradingCompany.DAL.Concrete;
using TradingCompany.Models;

namespace TradingCompany.Commands
{
    public class GetAllCommand : ICommand
    {
        public void Execute()
        {
            var categoryDal = new CategoryDAL();
            var allCategories = categoryDal.GetAll();

            Console.WriteLine("All categories:");
            foreach (var cat in allCategories)
            {
                Console.WriteLine($"Id: {cat.Id}, Name: {cat.Name}");
            }
        }
    }
}
