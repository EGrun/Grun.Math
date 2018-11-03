using System.Collections.Concurrent;

namespace Graph.Collections
{
    /// <inheritdoc cref="IStack{T}"/>
    public sealed class BigInMemoryStack<T> : IStack<T>
    {
        private ConcurrentStack<T> _internalStack;

        public BigInMemoryStack()
        {
            _internalStack = new ConcurrentStack<T>();
        }

        public bool IsEmpty
        {
            get
            {
                return _internalStack.IsEmpty;
            }
        }

        public T Peek()
        {
            _internalStack.TryPeek(out var result);
            return result;
        }

        public T Pop()
        {
            _internalStack.TryPop(out var result);
            return result;
        }

        public void Push(T item)
        {
            _internalStack.Push(item);
        }

        public bool TryPeek(out T result)
        {
            return _internalStack.TryPeek(out result);
        }

        public bool TryPop(out T result)
        {
            return _internalStack.TryPop(out result);
        }
    }
}
