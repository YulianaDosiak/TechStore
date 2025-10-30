using System;
using TechStore.Commands.Interfaces;
using TechStore.DAL.Interfaces;

namespace TechStore.Commands
{
    public class GetAllCommand<T> : ICommand where T : class
    {
        private readonly IGenericDAL<T> _dal;

        public GetAllCommand(IGenericDAL<T> dal)
        {
            _dal = dal;
        }

        public string Description => $"Get all {typeof(T).Name}";

        public void Execute()
        {
            var items = _dal.GetAll();
            if (items.Count > 0)
            {
                foreach (var item in items)
                {
                    Console.WriteLine(item);
                }
            }
            else
            {
                Console.WriteLine($"No {typeof(T).Name} found.");
            }
        }
    }
}