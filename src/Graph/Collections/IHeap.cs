using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Collections
{
    public interface IHeap<T>
    {
        int Count { get; }
        IHeapNode<T> Insert(T item);
        IHeapNode<T> Find(T item);
        IHeapNode<T> UpdateItem(IHeapNode<T> heapNode, T item);
        T ExamineMin();
        T ExtractMin();
        List<IHeapNode<T>> ToList();
    }
}
