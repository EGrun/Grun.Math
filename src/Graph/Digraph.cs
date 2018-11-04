using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph
{
    /// <inheritdoc cref="IDigraph{T}"/>
    public sealed class Digraph<T> : IDigraph<T>
    {
        private readonly Func<IEnumerable<T>> _vertices;
        private readonly Func<T, IEnumerable<T>> _forwardAdjacencies;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Graph.Digraph`1"/> class.
        /// </summary>
        /// <param name="vertices">Nodes in the graph.</param>
        /// <param name="forwardAdjacencies">Forward adjacencies.</param>
        public Digraph(Func<IEnumerable<T>> vertices, Func<T, IEnumerable<T>> forwardAdjacencies)
        {
            _vertices = vertices ?? throw new NullReferenceException(nameof(vertices));
            _forwardAdjacencies = forwardAdjacencies ?? throw new NullReferenceException(nameof(forwardAdjacencies));
        }

        public Digraph(T[] vertices, (T v1, T v2)[] edges)
        {
            _vertices = () => vertices;
            var lookup = edges.ToLookup(e => e.v1, e => e.v2);
            _forwardAdjacencies = v => lookup[v];
        }
        
        /// <inheritdoc />
        public IEnumerable<T> Vertices
        {
            get { return _vertices.Invoke(); }
        }
        
        /// <inheritdoc />
        public IEnumerable<T> ForwardAdjacencies(T node)
        {
            return _forwardAdjacencies(node);
        }
    }
}
