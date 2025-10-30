using System;

namespace TechStore.Commands.Interfaces
{
    public interface ICommand
    {
        void Execute();
        string Description { get; }
    }
}