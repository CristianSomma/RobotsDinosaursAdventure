using System;
using System.Collections.Generic;
using System.Text;

namespace RobotsDinosaursAdventure.Interfaces
{
    public interface IAsyncQueue<T>
    {
        Task<T> Peek();
        Task<T> Dequeue();
        Task Enqueue(T item);
    }
}
