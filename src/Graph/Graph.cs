using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Graph
{
    /// <summary>
    /// A set of Graph computations where a graph is defined as a source vertex
    /// and a function which computes the adjacency list for a vertex.
    /// This definition of graph is useful in situations where computing the entire graph
    /// is computationally or memory intensive or in distributed computing scenarios.
    /// Since edges are not precomputed, an algorithm may be shortcircuited in a more
    /// efficient manner than if the entire graph were loaded into memory.
    /// </summary>
    /// <example><code>
    /// //Define graph G:=(V,E) where:
    ///
    /// //set V of vertices in G
    /// int[] V = { 0, 1, 2 };
    ///
    /// //set E of edges in G
    /// var E = new(int v1, int v2)[] {(0,1), (0, 2)};
    ///
    /// //adjacency computation - define graph as function
    /// var lookup = E.ToLookup(e => e.v1, e => e.v2);
    /// Func<int, IEnumerable<int>> graph = (v) => lookup[v];
    ///
    /// Assert.Equal(false, graph.HasCycle(0));
    /// </code></example>
    public static class GraphExtensions
    {

        /// <summary>
        /// Determines if there exists a cycle in the graph containing the vertex
        /// </summary>
        /// <returns><c>true</c>, if cycle was detected, <c>false</c> otherwise.</returns>
        /// <param name="graph">Function to compute forward adjacency list for Vertex.</param>
        /// <param name="vertex">Vertex.</param>
        /// <typeparam name="T">The backing data type for vertices.</typeparam>
        public static Boolean HasCycle<T>(this Func<T, IEnumerable<T>> graph, T vertex)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph));
            Contract.EndContractBlock();

            if (object.ReferenceEquals(vertex, null))
                return false;

            return graph.HasCycle(vertex, EqualityComparer<T>.Default, 0);
        }

        /// <summary>
        /// Determines if there exists a cycle in the graph containing the vertex
        /// </summary>
        /// <returns><c>true</c>, if cycle was detected, <c>false</c> otherwise.</returns>
        /// <param name="graph">Function to compute forward adjacency list for Vertex.</param>
        /// <param name="vertex">Vertex.</param>
        /// <typeparam name="T">The backing data type for vertices.</typeparam>
        public static Boolean HasCycle<T>(this Func<T, IEnumerable<T>> graph, T vertex, Int32 capacity)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph));
            Contract.EndContractBlock();

            if (object.ReferenceEquals(vertex, null))
                return false;

            return graph.HasCycle(vertex, EqualityComparer<T>.Default, capacity);
        }

        /// <summary>
        /// Determines if there exists a cycle in the graph containing the vertex
        /// </summary>
        /// <returns><c>true</c>, if cycle was detected, <c>false</c> otherwise.</returns>
        /// <param name="graph">Function to compute forward adjacency list for Vertex.</param>
        /// <param name="vertex">Vertex.</param>
        /// <typeparam name="T">The backing data type for vertices.</typeparam>
        public static Boolean HasCycle<T>(this Func<T, IEnumerable<T>> graph, T vertex, IEqualityComparer<T> comparer)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph));
            Contract.EndContractBlock();

            if (object.ReferenceEquals(vertex, null))
                return false;

            return graph.HasCycle(vertex, comparer, 0);
        }

        /// <summary>
        /// Determines if there exists a cycle in the graph containing the vertex
        /// </summary>
        /// <returns><c>true</c>, if cycle was detected, <c>false</c> otherwise.</returns>
        /// <param name="graph">Function to compute forward adjacency list for Vertex.</param>
        /// <param name="source">Vertex.</param>
        /// <param name="comparer">Equality Comparer for vertex type.</param>
        /// <typeparam name="T">The backing data type for vertices.</typeparam>
        public static Boolean HasCycle<T>(
            this Func<T, IEnumerable<T>> graph, T source, IEqualityComparer<T> comparer, int capacity)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph));
            Contract.EndContractBlock();

            if (object.ReferenceEquals(source, null))
                return false;

            var resolved = new HashSet<T>(capacity, comparer);
            var backedges = new HashSet<T>(capacity, comparer);
            var toResolve = new Stack<T>(capacity);

            toResolve.Push(source);

            while (toResolve.Any())
            {
                var vertex = toResolve.Pop();

                if (resolved.Contains(vertex))
                {
                    continue;
                }

                backedges.Add(vertex);

                var forwardAdjacencies = graph(vertex).Where(fa => !resolved.Contains(fa));
                if (!forwardAdjacencies.Any())
                {
                    //base case
                    backedges.Remove(vertex);
                    resolved.Add(vertex);
                }

                else
                {
                    //recursive case
                    toResolve.Push(vertex); //push vertex back on to stack

                    foreach (var fa in forwardAdjacencies)
                    {
                        if (backedges.Contains(fa))
                            return true;

                        toResolve.Push(fa);
                    }
                }

            }
            return false;
        }
    }
}
