TreeTraversalExtensions
=======================

TreeTraversalExtensions is a small .NET library written in C# that converts any tree or hierarchal data structure into an enumerable list. It abstracts away the boilerplate recursive code you have to write to get at each node in a hierarchy.

TreeTraversalExtensions is useful if you find yourself writing virtually the same recursive methods over and over where the only differences are the action to perform at each node and where to get the child items. TreeTraversalExtensions handles the recursive piece of your code and lets you focus on the business logic. It works with any data type and you just tell it where to get the child items.

What makes this library even more powerful is once the hierarchy is flattened into an enumerable list, the full power of the LINQ library is availble for you to use. You can do searches with Where(), First(), or Any() or transform the data with Select(), GroupBy(), and Join().

TreeTraversalExtensions consists of two extension methods `ToEnumerable` and `ForEach`.

ToEnumerable()
--------------

Say you have the simple tree node type below.

```CSharp
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
```

You can flatten the hierarchy and then manipulate the nodes. All you need to do is indicate how to get the child items. In this case, the lambda function `n => n.ChildItems`.

```CSharp
    var rootTreeNode = new TreeNode(1,
        new List<TreeNode>()
        {
            new TreeNode(2,
                new List<TreeNode>()
                {
                    new TreeNode(3),
                    new TreeNode(4),
                    new TreeNode(5)
                }),
            new TreeNode(6,
                new List<TreeNode>()
                {
                    new TreeNode(7),
                    new TreeNode(8),
                    new TreeNode(9)
                }),
            new TreeNode(10,
                new List<TreeNode>()
                {
                    new TreeNode(11),
                    new TreeNode(12),
                    new TreeNode(13)
                }),
        });

    // Use ToEnumerable() to flatten the tree into an enumerable list.
    var allNodes = rootTreeNode.ToEnumerable(n => n.ChildItems);

    // You can then use LINQ to search.
    var node9 = allNodes.First(n => n.Index == 9);
    Assert.That(node9.Index, Is.EqualTo(9));

    var oddNodeCount = allNodes.Count(n => n.Index%2 != 0);
    Assert.That(oddNodeCount, Is.EqualTo(7));

    // Or use LINQ to transform the data.
    var intList = allNodes.Select(n => n.Index);
    CollectionAssert.AreEqual(Enumerable.Range(1, 13), intList);
```

You can traverse the hiearchy either depth-first (default) or breadth-first.

```CSharp
    // Depth-first traversal (default)
    var items = rootTreeNode.ToEnumerable(n => n.ChildItems);

    // Breadth-first traversal
    items = rootTreeNode.ToEnumerable(TraversalType.BreadthFirst, n => n.ChildItems);
```





