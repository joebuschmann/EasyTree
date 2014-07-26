using System.Collections.Generic;
using System.Linq;

namespace TreeTraversalExtensions.Tests
{
    public class TreeNode
    {
        private readonly int _index;
        private readonly IEnumerable<TreeNode> _childItems;

        public TreeNode(int index, IEnumerable<TreeNode> childItems = null)
        {
            _index = index;
            _childItems = childItems == null ? Enumerable.Empty<TreeNode>() : childItems.ToList();
        }

        public int Index
        {
            get { return _index; }
        }

        public IEnumerable<TreeNode> ChildItems
        {
            get { return _childItems; }
        }
    }
}