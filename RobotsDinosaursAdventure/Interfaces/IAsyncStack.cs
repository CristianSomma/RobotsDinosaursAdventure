using CollectionsLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RobotsDinosaursAdventure.Interfaces
{
    public interface IAsyncStack<T>
    {
        Task Push(T item);
        Task<T> Peek();
        Task<T> Pop();
    }
}
