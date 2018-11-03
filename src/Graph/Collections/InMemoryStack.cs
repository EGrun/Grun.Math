using System;
using System.Collections.Generic;

namespace Graph.Collections
{
    /// <inheritdoc cref="IStack{T}"/>
    public class InMemoryStack<T> : IStack<T>
    {
        private Stack<T> _internalStack;

        public InMemoryStack()
        {
            _internalStack = new Stack<T>();
        }

        public int Count
        {
            get { return _internalStack.Count; }
        }

        public T Peek()
        {
            return _internalStack.Peek();
        }

        public T Pop()
        {
            return _internalStack.Pop();
        }

        public void Push(T item)
        {
            _internalStack.Push(item);
        }

        public bool TryPeek(out T result)
        {
            try
            {
                result = _internalStack.Peek();
                return true;
            }
            catch (InvalidOperationException)
            {
                result = default;
                return false;
            }
        }

        public bool TryPop(out T result)
        {
            try
            {
                result = _internalStack.Pop();
                return true;
            }
            catch (InvalidOperationException)
            {
                result = default;
                return false;
            }
        }
    }
}
