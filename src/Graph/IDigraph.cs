using System.Collections.Generic;

namespace Graph
{
    /// <summary>
    /// Directed Graph consisting of a set of vertices and a method for
    /// resolving the set of forward directed adjacent edges.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDigraph<T>
    {
        /// <summary>
        /// Collection of nodes that comprise the graph
        /// </summary>
        IEnumerable<T> Vertices { get; }

        /// <summary>
        /// The set of all vertices adjecent to this one on the directed graph
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
         IEnumerable<T> ForwardAdjacencies(T node);
    }
}
