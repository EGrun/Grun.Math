namespace Graph.Collections
{
    /// <summary>
    /// Generic collection that guarantees the uniqueness of its elements, as defined
    /// by some comparer.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISet<T>
    {
        /// <summary>
        /// Adds the item to the <see cref="ISet{T}"/>.
        /// </summary>
        /// <param name="item">The item to add.</param>
        void Add(T item);

        /// <summary>
        /// Remove the specified item from the <see cref="ISet{T}"/>.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        /// <exception cref="InvalidOperationException">The item is not a member of the <see cref="ISet{T}"/>.</exception>
        void Remove(T item);

        /// <summary>
        /// Returns if the items belongs to the <see cref="ISet{T}"/>.
        /// </summary>
        /// <param name="item">The item to check for presence in the <see cref="ISet{T}"/>.</param>
        /// <returns>True, if the item belongs to the <see cref="ISet{T}"/>.</returns>
        bool Contains(T item);
    }
}
