using System;
using System.Collections;
using System.Collections.Generic;

namespace Graph
{
    /// <summary>
    /// Algorithm for calculating shortest path between two vertex on a weighted graph.
    /// Requires no negative weighted paths.
    /// </summary>
    /// <typeparam name="TVertex"></typeparam>
    public class Djikstras<TVertex>
    {
        private IDigraph<TVertex> _graph;

        public Djikstras(IDigraph<TVertex> graph)
        {
            _graph = graph ?? throw new ArgumentNullException(nameof(graph));
        }

        public IList<IDigraph<TVertex>> GetShortestPath(TVertex source, TVertex target)
        {
            if (ReferenceEquals(source, null))
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (ReferenceEquals(target, null))
            {
                throw new ArgumentNullException(nameof(target));
            }

            var currentWeights = new double[_graph.Edges.Count];
            var checkedVertices = new bool[_graph.Edges.Count];

            

        }
    }
}
