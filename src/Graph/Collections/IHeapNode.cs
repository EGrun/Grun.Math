using System;
namespace Graph.Collections
{
    public interface IHeapNode<out T>
    {
        T Item { get; }
    }
}
