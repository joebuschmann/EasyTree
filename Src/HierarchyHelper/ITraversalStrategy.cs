using System.Collections.Generic;

namespace HierarchyHelper
{
    internal interface ITraversalStrategy<T>
    {
        bool HasMoreItems { get; }
        T GetNextItem();
        void AddItem(T item);
        IEnumerable<T> GetChildren(T currentItem);
    }
}