using System;

namespace Graph.Collections
{
    /// <inheritdoc cref="IStack{T}"/>
    public sealed class BigInMemoryStack<T> : IStack<T>
    {
        /// <summary>
        /// A simple (internal) node type used to store elements of <see cref="BigInMemoryStack{T}"/>.
        /// </summary>
        private sealed class Node
        {
            internal readonly T _value; // Value of the node.
            internal Node _next; // Next pointer.

            /// <summary>
            /// Constructs a new node with the specified value and no next node.
            /// </summary>
            /// <param name="value">The value of the node.</param>
            internal Node(T value)
            {
                _value = value;
                _next = null;
            }
        }

        private Node _head;

        public bool IsEmpty
        {
            get
            {
                return _head is null;
            }
        }

        public T Peek()
        {
            if (_head is null)
            {
                throw new InvalidOperationException("Stack is empty.");
            }
            return _head._value;
        }

        public T Pop()
        {
            if (_head is null)
            {
                throw new InvalidOperationException("Stack is empty.");
            }
            var item = _head._value;
            _head = _head._next;
            return item;
        }

        public void Push(T item)
        {
            var newNode = new Node(item)
            {
                _next = _head,
            };
            _head = newNode;
        }

        public bool TryPeek(out T result)
        {
            if (_head is null)
            {
                result = default;
                return false;
            }
            result = _head._value;
            return true;
        }

        public bool TryPop(out T result)
        {
            if (_head is null)
            {
                result = default;
                return false;
            }
            result = _head._value;
            _head = _head._next;
            return true;
        }
    }
}
