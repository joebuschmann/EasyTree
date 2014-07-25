using System;
using System.Collections.Generic;

namespace HierarchyHelper
{
    internal class BreadthFirstStrategy<T> : ITraversalStrategy<T>
    {
        private readonly Func<T, IEnumerable<T>> _getChildrenFunc;
        private readonly Queue<T> _queue;

        public BreadthFirstStrategy(Func<T, IEnumerable<T>> getChildrenFunc)
        {
            _getChildrenFunc = getChildrenFunc;
            _queue = new Queue<T>();
        }

        public bool HasMoreItems
        {
            get { return _queue.Count > 0; }
        }

        public T GetNextItem()
        {
            return _queue.Dequeue();
        }

        public void AddItem(T item)
        {
            _queue.Enqueue(item);
        }

        public IEnumerable<T> GetChildren(T currentItem)
        {
            return _getChildrenFunc(currentItem);
        }
    }
}
