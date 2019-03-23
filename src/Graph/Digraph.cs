using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph
{
    /// <inheritdoc cref="IDigraph{TVertex}"/>
    public sealed class Digraph<TVertex> : IDigraph<TVertex>
    {
        private readonly Func<IEnumerable<TVertex>> _vertices;
        private readonly Func<TVertex, IEnumerable<IEdge<TVertex>>> _connectedEdges;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Graph.Digraph`1"/> class.
        /// </summary>
        /// <param name="vertices">Factory that produces Nodes in the graph.</param>
        /// <param name="connectedEdges">Func that calculates Edges connected to supplied node.</param>
        public Digraph(Func<IEnumerable<TVertex>> vertices, Func<TVertex, IEnumerable<IEdge<TVertex>>> connectedEdges)
        {
            _vertices = vertices ?? throw new NullReferenceException(nameof(vertices));
            _connectedEdges = connectedEdges ?? throw new NullReferenceException(nameof(connectedEdges));
        }

        public Digraph(TVertex[] vertices, (TVertex v1, TVertex v2)[] edges)
        {
            _vertices = () => vertices;
            var lookup = edges.ToLookup(e => e.v1, e => new Edge<TVertex>(e.v1, e.v2));
            _connectedEdges = v => lookup[v];
        }
        
        /// <inheritdoc />
        public IEnumerable<TVertex> Vertices => _vertices.Invoke();

        /// <inheritdoc />
        public IEnumerable<IEdge<TVertex>> ConnectedEdges(TVertex node)
        {
            return _connectedEdges(node);
        }
    }
}
