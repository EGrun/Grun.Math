using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph.Collections
{
    /// <inheritdoc cref="ISet{T}"/>
    public sealed class BigInMemorySet<T> : ISet<T>
    {
        private List<HashSet<T>> _setCollection;
        private HashSet<T> _curHashSet;
        private Func<HashSet<T>> _hashSetFactory;

        public BigInMemorySet()
        {
            _hashSetFactory = () => new HashSet<T>();
            Initialize();
        }

        public BigInMemorySet(IEqualityComparer<T> comparer)
        {
            _hashSetFactory = () => new HashSet<T>(comparer);
            Initialize();
        }

        private void Initialize()
        {
            _curHashSet = _hashSetFactory.Invoke();
            _setCollection = new List<HashSet<T>>
            {
                _curHashSet
            };
        }
        
        public void Add(T item)
        {
            try
            {
                _curHashSet.Add(item);
            }
            catch (OutOfMemoryException)
            {
                _curHashSet = _hashSetFactory.Invoke();
                _curHashSet.Add(item);
                _setCollection.Add(_curHashSet);
            }
        }

        public void Remove(T item)
        {
            foreach (var hashSet in _setCollection)
            {
                hashSet.Remove(item);
            }
        }

        public bool Contains(T item)
        {
            return _setCollection.Any(hashSet => hashSet.Contains(item));
        }
    }
}
