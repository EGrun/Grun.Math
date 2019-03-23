using System;
using System.Runtime.Serialization;

namespace Graph.Collections
{
    public class PriorityStack<T> : IStack<T>
    {
        private readonly IHeap<T> _heap;

        public PriorityStack () : this (new PairingHeap<T>())
        {
        }

        public PriorityStack(IHeap<T> heap)
        {
            if (object.ReferenceEquals(heap, null))
            {
                throw new ArgumentNullException(nameof(heap));
            }

            _heap = heap;
        }

        public bool IsEmpty => _heap.Count == 0;

        public void Push(T item)
        {
            _heap.Insert(item);
        }

        public T Peek()
        {
            return _heap.ExamineMin();
        }

        public T Pop()
        {
            return _heap.ExtractMin();
        }

        public void Update(T item)
        {
            var found = _heap.Find(item);

            if (object.ReferenceEquals(found, null))
            {
                throw new InvalidOperationException("item not found in collection.");
            }

            _heap.UpdateItem(found, item);
        }
    }
}
