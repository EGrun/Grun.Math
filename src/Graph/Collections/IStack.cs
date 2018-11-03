namespace Graph.Collections
{
    public interface IStack<T>
    {
        /// <summary>
        /// Gets a value that indicates whether the <see cref="IStack{T}"/> is empty.
        /// </summary>
        /// <returns></returns>
        bool IsEmpty { get; }

        /// <summary>
        /// Inserts an object at the top of the <see cref="IStack{T}"/>.
        /// </summary>
        /// <param name="item">The object to push onto the <see cref="IStack{T}"/>. The value can be <c>null</c> for reference types.</param>
        void Push(T item);

        /// <summary>
        /// Returns the object at the top of the <see cref="IStack{T}"/> without removing it.
        /// </summary>
        /// <returns>The object at the top of the <see cref="IStack{T}"/>.</returns>
        /// <exception cref="InvalidOperationException">The <see cref="IStack{T}"/> is empty.</exception>  
        T Peek();

        /// <summary>
        /// Removes and returns the object at the top of the <see cref="IStack{T}"/>.
        /// </summary>
        /// <returns>The object removed from the top of the <see cref="IStack{T}"/>.</returns>
        /// <exception cref="InvalidOperationException">The <see cref="IStack{T}"/> is empty.</exception> 
        T Pop();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        bool TryPeek(out T result);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        bool TryPop(out T result);
    }
}
