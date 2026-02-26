using System;
using System.Collections.Generic;
using System.Text;

namespace RobotsDinosaursAdventure.Interfaces
{
    public interface IAsyncContainer<T>
    {
        void Build(IEnumerable<T> items);
        Task Clear();
        Task<int> GetLength();
        Task<bool> IsEmpty();
    }
}
