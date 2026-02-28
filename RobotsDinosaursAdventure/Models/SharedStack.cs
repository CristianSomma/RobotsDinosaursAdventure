using RobotsDinosaursAdventure.Interfaces;
using System;
using System.Text;

namespace RobotsDinosaursAdventure.Models
{
    public class SharedStack<T> 
        : IAsyncContainer<T>, IAsyncStack<T>
    {
        private CollectionsLibrary.Collections.Stack<T> _stack;
        private readonly SemaphoreSlim _mutex, _itemsAvailable;
        private readonly CancellationToken _token;

        public SharedStack(CancellationToken token = default)
        {
            _stack = new CollectionsLibrary.Collections.Stack<T>();
            _mutex = new SemaphoreSlim(1, 1);
            _itemsAvailable = new SemaphoreSlim(0);
            _token = token;
        }

        public SharedStack(IEnumerable<T> items, CancellationToken token = default)
        {
            Build(items);
            _mutex = new SemaphoreSlim(1, 1);
            _itemsAvailable = new SemaphoreSlim(items.Count());
            _token = token;
        }

        public void Build(IEnumerable<T> items)
        {
            _stack = new CollectionsLibrary.Collections.Stack<T>(items);
        }

        public async Task Clear()
        {
            await _mutex.WaitAsync(_token);

            try
            {
                ClearAndReset();
            }
            finally
            {
                _mutex.Release();
            }
        }

        public async Task<int> GetLength()
        {
            await _mutex.WaitAsync(_token);

            try
            {
                return _stack.GetLength();
            }
            finally
            {
                _mutex.Release();
            }
        }

        public async Task<bool> IsEmpty()
        {
            await _mutex.WaitAsync(_token);

            try
            {
                return _stack.IsEmpty();
            }
            finally
            {
                _mutex.Release();
            }
        }

        public async Task Push(T item)
        {
            await _mutex.WaitAsync(_token);

            try
            {
                _stack.Push(item);
                _itemsAvailable.Release();
            }
            finally
            {
                _mutex.Release();
            }
        }

        public async Task<T> Peek()
        {
            await _mutex.WaitAsync(_token);

            try
            {
                return _stack.Peek();
            }
            finally
            {
                _mutex.Release();
            }
        }

        public async Task<T> Pop()
        {
            await _itemsAvailable.WaitAsync(_token);
            
            await _mutex.WaitAsync(_token);

            try
            {
                return _stack.Pop();
            }
            finally
            {
                _mutex.Release();
            }
        }

        public async Task<bool> TryClear(Predicate<int> predicate)
        {
            await _mutex.WaitAsync(_token);

            try
            {
                if (predicate(_stack.GetLength()))
                {
                    ClearAndReset();
                    return true;
                }

                return false;
            }
            finally
            {
                _mutex.Release();
            }
        }

        private void ClearAndReset()
        {
            _stack.Clear();

            while (_itemsAvailable.CurrentCount > 0)
                _itemsAvailable.Wait(0);
        }
    }
}
