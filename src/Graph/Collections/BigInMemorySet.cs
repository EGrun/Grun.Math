using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph.Collections
{
    /// <inheritdoc cref="ISet{T}"/>
    public class BigInMemorySet<T> : ISet<T>
    {
        private List<HashSet<T>> _setCollection;
        private int _index = 0;
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
            _setCollection = new List<HashSet<T>>
            {
                _hashSetFactory.Invoke()
            };
            _index = 0;
        }
        
        public void Add(T item)
        {
            try
            {
                _setCollection[_index].Add(item);
            }
            catch (OutOfMemoryException)
            {
                var hashSet = _hashSetFactory.Invoke();
                hashSet.Add(item);
                _setCollection.Add(hashSet);
                _index++;
            }
        }

        public void Remove(T item)
        {
            _setCollection.AsParallel().ForAll(hashSet => hashSet.Remove(item));
        }

        public bool Contains(T item)
        {
            return _setCollection.AsParallel().Any(hashSet => hashSet.Contains(item));
        }
    }
}
