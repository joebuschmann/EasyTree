using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyTree
{
    internal class DepthFirstStrategy<T> : ITraversalStrategy<T>
    {
        private readonly Func<T, IEnumerable<T>> _getChildrenFunc;
        private readonly Stack<T> _stack;

        public DepthFirstStrategy(Func<T, IEnumerable<T>> getChildrenFunc)
        {
            _getChildrenFunc = getChildrenFunc;
            _stack = new Stack<T>();
        }

        public bool HasMoreItems
        {
            get { return _stack.Count > 0; }
        }

        public T GetNextItem()
        {
            return _stack.Pop();
        }

        public void AddItem(T item)
        {
            _stack.Push(item);
        }

        public IEnumerable<T> GetChildren(T currentItem)
        {
            return _getChildrenFunc(currentItem).Reverse();
        }
    }
}
