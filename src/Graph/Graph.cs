using System;
using System.Collections.Generic;
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
    public class Digraph<T>
    {

        readonly Func<T, IEnumerable<T>> _forwardAdjacencies;
        readonly IEqualityComparer<T> _comparer;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Graph.Digraph`1"/> class.
        /// </summary>
        /// <param name="forwardAdjacencies">Forward adjacencies.</param>
        public Digraph(Func<T, IEnumerable<T>> forwardAdjacencies)
            : this(forwardAdjacencies, EqualityComparer<T>.Default)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Graph.Digraph`1"/> class.
        /// </summary>
        /// <param name="forwardAdjacencies">Forward adjacencies.</param>
        /// <param name="comparer">Comparer</param>
        public Digraph(Func<T, IEnumerable<T>> forwardAdjacencies, IEqualityComparer<T> comparer)
        {
            _forwardAdjacencies = forwardAdjacencies ?? throw new NullReferenceException(nameof(forwardAdjacencies));
            _comparer = comparer ?? throw new NullReferenceException(nameof(comparer));
        }

        /// <summary>
        /// Determines if there exists a cycle in the graph containing the origin vertex
        /// </summary>if (object.ReferenceEquals(origin, null))
        /// <param name="origin">Origin vertex.</param>
        /// <returns><c>true</c>, if cycle was detected, <c>false</c> otherwise.</returns>
        public virtual bool HasCycle(T origin)
        {
            return HasCycle(origin, 0);
        }

        /// <summary>
        /// Determines if there exists a cycle in the graph containing the origin vertex
        /// </summary>
        /// <returns><c>true</c>, if cycle was detected, <c>false</c> otherwise.</returns>
        /// <param name="origin">Origin vertex.</param>
        /// <param name="capacity">Initial capacity of working data containers</param>
        public virtual bool HasCycle(T origin, int capacity)
        {
            var skipList = new HashSet<T>(_comparer);
            var knownBackEdges = new HashSet<T>(_comparer);
            skipList.SetCapacity(capacity);
            knownBackEdges.SetCapacity(capacity);

            return HasCycle(origin, knownBackEdges, skipList);
        }

        /// <summary>
        /// Determines if there exists a cycle in the graph containing the origin vertex
        /// </summary>
        /// <returns><c>true</c>, if cycle was detected, <c>false</c> otherwise.</returns>
        /// <param name="origin">Origin.</param>
        /// <param name="knownBackEdges">List of known back edges.</param>
        /// <param name="skipList">Edges to skip.</param>
        protected bool HasCycle(T origin, ICollection<T> knownBackEdges, ICollection<T> skipList)
        {

            var stack = new Stack<T>(new[] { origin });

            while (stack.Any())
            {
                var vertex = stack.Pop();

                if (skipList.Contains(vertex))
                {
                    continue;
                }

                knownBackEdges.Add(vertex);

                var fa = _forwardAdjacencies(vertex)?.Where(adj => !skipList.Contains(adj));
                if (fa is null || !fa.Any())
                {
                    //base case
                    knownBackEdges.Remove(vertex);
                    skipList.Add(vertex);
                }

                else
                {
                    //recursive case
                    stack.Push(vertex); //push vertex back on to stack

                    foreach (var adj in fa)
                    {
                        if (knownBackEdges.Contains(adj))
                            return true;

                        stack.Push(adj);
                    }
                }

            }
            return false;
        }
    }
}
