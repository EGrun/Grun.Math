using System;
using System.Linq;
using Graph.Collections;

namespace Graph
{
    public sealed class CycleAnalyzer<T>
    {
        private readonly Func<ISet<T>> _setFactory;
        private readonly Func<IStack<T>> _stackFactory;
        private readonly IDigraph<T> _digraph;

        public CycleAnalyzer (IDigraph<T> digraph)
            : this (digraph, () => new InMemorySet<T>(), () => new InMemoryStack<T>())
        {
        }

        public CycleAnalyzer(IDigraph<T> digraph, Func<ISet<T>> setFactory, Func<IStack<T>> stackFactory)
        {
            _digraph = digraph ?? throw new ArgumentNullException(nameof(digraph));
            _setFactory = setFactory ?? throw new ArgumentNullException(nameof(setFactory));
            _stackFactory = stackFactory ?? throw new ArgumentNullException(nameof(stackFactory));
        }

        /// <summary>
        /// Determines if there exists a cycle in the graph accessible from the origin vertex
        /// </summary>if (object.ReferenceEquals(origin, null))
        /// <param name="origin">Origin vertex.</param>
        /// <returns><c>true</c>, if cycle was detected, <c>false</c> otherwise.</returns>
        public bool HasCycle(T origin)
        {
            var knownAcyclicVertices = _setFactory.Invoke();
            var knownBackEdges = _setFactory.Invoke();
            var workingStack = _stackFactory.Invoke();
            workingStack.Push(origin);
            return HasCycle(knownBackEdges, knownAcyclicVertices, workingStack);
        }

        /// <summary>
        /// Determines if there exists a cycle in the graph accessible from the origin vertex
        /// </summary>
        /// <returns><c>true</c>, if cycle was detected, <c>false</c> otherwise.</returns>
        /// <param name="origin">Origin.</param>
        /// <param name="knownBackEdges">List of known back edges directed to the origin.</param>
        /// <param name="knownVertices">List of known vertices that have previously been calculated to have no cycle</param>
        private bool HasCycle(ISet<T> knownBackEdges, ISet<T> knownAcyclicVertices, IStack<T> workingStack)
        {
            while (!workingStack.IsEmpty)
            {
                var vertex = workingStack.Pop();
                if (knownAcyclicVertices.Contains(vertex))
                {
                    continue;
                }
                knownBackEdges.Add(vertex);

                var fa = _digraph.ConnectedEdges(vertex)?.Where(adj => !knownAcyclicVertices.Contains(adj.Target));
                if (fa is null || !fa.Any())
                {
                    // base case
                    knownBackEdges.Remove(vertex);
                    knownAcyclicVertices.Add(vertex);
                }
                else
                {
                    // recursive case
                    workingStack.Push(vertex);
                    foreach (var adj in fa)
                    {
                        if (knownBackEdges.Contains(adj.Target))
                        {
                            return true;
                        }
                        workingStack.Push(adj.Target);
                    }
                }
            }
            return false;
        }
    }
}
