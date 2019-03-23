using System;
using System.Diagnostics.Contracts;

namespace Graph
{
    public class Edge<TVertex> : IEdge<TVertex>
    {
        public Edge (TVertex source, TVertex target)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }
            Contract.EndContractBlock();

            Source = source;
            Target = target;
        }

        public TVertex Source { get; private set; }

        public TVertex Target { get; private set; }
    }
}
