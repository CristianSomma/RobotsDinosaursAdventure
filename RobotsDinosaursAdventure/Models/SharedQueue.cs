using CollectionsLibrary.Collections;
using CollectionsLibrary.Interfaces;
using RobotsDinosaursAdventure.Interfaces;

namespace RobotsDinosaursAdventure.Models
{
    public class SharedQueue<T> 
        : IAsyncContainer<T>, IAsyncQueue<T>
    {
        private CollectionsLibrary.Collections.Queue<T> _queue;
        private readonly SemaphoreSlim _mutex, _itemAvailable;
        private readonly CancellationToken _token;

        public SharedQueue(CancellationToken token)
        {
            _queue = new CollectionsLibrary.Collections.Queue<T>();
            _mutex = new SemaphoreSlim(1, 1);
            _itemAvailable = new SemaphoreSlim(0);
            _token = token;
        }

        public SharedQueue(IEnumerable<T> items, CancellationToken token)
        {
            Build(items);
            _mutex = new SemaphoreSlim(1, 1);
            _itemAvailable = new SemaphoreSlim(items.Count());
            _token = token;
        }

        public void Build(IEnumerable<T> items)
        {
            _queue = new CollectionsLibrary.Collections.Queue<T>(items);
        }

        public async Task Clear()
        {
            await _mutex.WaitAsync(_token);

            try
            {
                _queue.Clear();
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
                return _queue.GetLength();
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
                return _queue.IsEmpty();
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
                return _queue.Peek();
            }
            finally
            {
                _mutex.Release();
            }
        }

        public async Task<T> Dequeue()
        {
            await _itemAvailable.WaitAsync(_token);

            await _mutex.WaitAsync(_token);

            try
            {
                return _queue.Dequeue();
            }
            finally
            {
                _mutex.Release();
            }
        }

        public async Task Enqueue(T item)
        {
            await _mutex.WaitAsync(_token);

            try
            {
                _queue.Enqueue(item);
                _itemAvailable.Release();
            }
            finally
            {
                _mutex.Release();
            }
        }

    }
}
