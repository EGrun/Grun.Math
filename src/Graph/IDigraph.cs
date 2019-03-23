using System.Collections.Generic;

namespace Graph
{
    /// <summary>
    /// Directed Graph consisting of a set of vertices and a method for
    /// resolving the set of forward directed adjacent edges.
    /// </summary>
    /// <typeparam name="TVertex"></typeparam>
    public interface IDigraph<TVertex>
    {
        /// <summary>
        /// Collection of nodes that comprise the graph
        /// </summary>
        IEnumerable<TVertex> Vertices { get; }

        /// <summary>
        /// The set of all edges connected to supplied node on the directed graph.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        IEnumerable<IEdge<TVertex>> ConnectedEdges(TVertex node);
    }
}
