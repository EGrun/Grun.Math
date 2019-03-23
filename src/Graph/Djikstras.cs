using System;
using System.Collections;
using System.Collections.Generic;
using Graph.Collections;

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

        private Func<IEdge<TVertex>, double> _weightRelaxer;

        public Djikstras(IDigraph<TVertex> graph, Func<IEdge<TVertex>, double> weightRelaxer)
        {
            _graph = graph ?? throw new ArgumentNullException(nameof(graph));
            _weightRelaxer = weightRelaxer ?? throw new ArgumentNullException(nameof(weightRelaxer));
        }

        public IList<IEdge<TVertex>> GetShortestPath(TVertex source, TVertex target)
        {
            if (ReferenceEquals(source, null))
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (ReferenceEquals(target, null))
            {
                throw new ArgumentNullException(nameof(target));
            }

            var currentWeights = new Dictionary<TVertex, double>();
            var currentPaths = new Dictionary<TVertex, IList<IEdge<TVertex>>>();
            var visited = new Dictionary<TVertex, bool>();

            var priorityHeap = new PairingHeap<TVertex>(
                (v1, v2) => currentWeights[v1].CompareTo(currentWeights[v1]));

            var priorityStack = new PriorityStack<TVertex>(priorityHeap);

            currentWeights[source] = 0;
            currentPaths[source] = new List<IEdge<TVertex>> ();
            priorityStack.Push(source);

            while (!priorityStack.IsEmpty)
            {
                var node = priorityStack.Pop();

                foreach (var edge in _graph.ConnectedEdges(node))
                {
                    var weight = _weightRelaxer(edge) + currentWeights[node];
                    var destination = edge.Target;

                    if (EqualityComparer<TVertex>.Default.Equals(target, destination))
                    {
                        return new List<IEdge<TVertex>>(currentPaths[node]) { edge };
                    }

                    var isNew = !currentWeights.ContainsKey(destination);
                    if (isNew || currentWeights[destination] < weight)
                    {
                        currentWeights[destination] = weight;
                        var currentPath = new List<IEdge<TVertex>>(currentPaths[node]) { edge };
                        currentPaths[destination] = currentPath;

                        if (isNew)
                        {
                            priorityStack.Push(destination);
                        }
                        else if (!visited.ContainsKey(destination))
                        {
                            priorityStack.Update(destination);
                        }
                    }

                    visited[node] = true;
                }
            }

            throw new InvalidOperationException("No path path found from source vertex to target vertex.");
        }
    }
}
