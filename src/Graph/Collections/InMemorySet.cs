using System.Collections.Generic;

namespace Graph.Collections
{
    /// <inheritdoc cref="ISet{T}"/>
    public class InMemorySet<T> : ISet<T>
    {
        private HashSet<T> _internalSet;

        public InMemorySet()
        {
            _internalSet = new HashSet<T>();
        }

        public InMemorySet(IEqualityComparer<T> comparer)
        {
            _internalSet = new HashSet<T>(comparer);
        }
        
        public void Add(T item)
        {
            _internalSet.Add(item);
        }

        public void Remove(T item)
        {
            _internalSet.Remove(item);
        }

        public bool Contains(T item)
        {
            return _internalSet.Contains(item);
        }
    }
}
