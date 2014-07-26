using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace EasyTree.Tests
{
    [TestFixture]
    public class HierarchyExtensionsFixture
    {
        [Test]
        public void ValidateDepthFirstTraversalSingleRoot()
        {
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

            var TreeNodes = rootTreeNode.ToEnumerable(i => i.ChildItems);
            CollectionAssert.AreEqual(Enumerable.Range(1, 13), TreeNodes.Select(i => i.Index));
        }

        [Test]
        public void ValidateDepthFirstTraversalMultipleRoot()
        {
            var rootTreeNode1 = new TreeNode(1,
                new List<TreeNode>()
                {
                    new TreeNode(2,
                        new List<TreeNode>()
                        {
                            new TreeNode(3),
                            new TreeNode(4),
                            new TreeNode(5)
                        }),
                });

            var rootTreeNode2 = new TreeNode(6,
                new List<TreeNode>()
                {
                    new TreeNode(7,
                        new List<TreeNode>()
                        {
                            new TreeNode(8),
                            new TreeNode(9),
                            new TreeNode(10)
                        }),
                });

            var rootTreeNodes = new List<TreeNode>() {rootTreeNode1, rootTreeNode2};
            var TreeNodes = rootTreeNodes.ToEnumerable(i => i.ChildItems);

            CollectionAssert.AreEqual(Enumerable.Range(1, 10), TreeNodes.Select(i => i.Index));
        }

        [Test]
        public void ValidateBreadthFirstTraversalSingleRoot()
        {
            var rootTreeNode = new TreeNode(1,
                new List<TreeNode>()
                {
                    new TreeNode(2,
                        new List<TreeNode>()
                        {
                            new TreeNode(5),
                            new TreeNode(6),
                            new TreeNode(7)
                        }),
                    new TreeNode(3,
                        new List<TreeNode>()
                        {
                            new TreeNode(8),
                            new TreeNode(9),
                            new TreeNode(10)
                        }),
                    new TreeNode(4,
                        new List<TreeNode>()
                        {
                            new TreeNode(11),
                            new TreeNode(12),
                            new TreeNode(13)
                        }),
                });

            var TreeNodes = rootTreeNode.ToEnumerable(TraversalType.BreadthFirst, i => i.ChildItems);

            CollectionAssert.AreEqual(Enumerable.Range(1, 13), TreeNodes.Select(i => i.Index));
        }

        [Test]
        public void ValidateBreadthFirstTraversalMultipleRoot()
        {
            var rootTreeNode1 = new TreeNode(1,
                new List<TreeNode>()
                {
                    new TreeNode(2,
                        new List<TreeNode>()
                        {
                            new TreeNode(4),
                            new TreeNode(5),
                            new TreeNode(6)
                        }),
                    new TreeNode(3,
                        new List<TreeNode>()
                        {
                            new TreeNode(7),
                            new TreeNode(8),
                            new TreeNode(9)
                        }),
                });

            var rootTreeNode2 = new TreeNode(10,
                new List<TreeNode>()
                {
                    new TreeNode(11,
                        new List<TreeNode>()
                        {
                            new TreeNode(13),
                            new TreeNode(14),
                            new TreeNode(15)
                        }),
                    new TreeNode(12,
                        new List<TreeNode>()
                        {
                            new TreeNode(16),
                            new TreeNode(17),
                            new TreeNode(18)
                        }),
                });

            var rootTreeNodes = new List<TreeNode>() { rootTreeNode1, rootTreeNode2 };
            var TreeNodes = rootTreeNodes.ToEnumerable(TraversalType.BreadthFirst, i => i.ChildItems);

            CollectionAssert.AreEqual(Enumerable.Range(1, 18), TreeNodes.Select(i => i.Index));
        }

        [Test]
        public void ValidateDepthFirstForEachSingleRoot()
        {
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

            var results = new List<int>();
            rootTreeNode.ForEach(i => i.ChildItems, i => results.Add(i.Index));

            CollectionAssert.AreEqual(Enumerable.Range(1, 13), results);
        }

        [Test]
        public void ValidateDepthFirstForEachMultipleRoot()
        {
            var rootTreeNode1 = new TreeNode(1,
                new List<TreeNode>()
                {
                    new TreeNode(2,
                        new List<TreeNode>()
                        {
                            new TreeNode(3),
                            new TreeNode(4),
                            new TreeNode(5)
                        }),
                });

            var rootTreeNode2 = new TreeNode(6,
                new List<TreeNode>()
                {
                    new TreeNode(7,
                        new List<TreeNode>()
                        {
                            new TreeNode(8),
                            new TreeNode(9),
                            new TreeNode(10)
                        }),
                });

            var results = new List<int>();
            var rootTreeNodes = new List<TreeNode>() { rootTreeNode1, rootTreeNode2 };
            rootTreeNodes.ForEach(i => i.ChildItems, i => results.Add(i.Index));

            CollectionAssert.AreEqual(Enumerable.Range(1, 10), results);
        }

        [Test]
        public void ValidateBreadthFirstForEachSingleRoot()
        {
            var rootTreeNode = new TreeNode(1,
                new List<TreeNode>()
                {
                    new TreeNode(2,
                        new List<TreeNode>()
                        {
                            new TreeNode(5),
                            new TreeNode(6),
                            new TreeNode(7)
                        }),
                    new TreeNode(3,
                        new List<TreeNode>()
                        {
                            new TreeNode(8),
                            new TreeNode(9),
                            new TreeNode(10)
                        }),
                    new TreeNode(4,
                        new List<TreeNode>()
                        {
                            new TreeNode(11),
                            new TreeNode(12),
                            new TreeNode(13)
                        }),
                });

            var results = new List<int>();
            rootTreeNode.ForEach(TraversalType.BreadthFirst, i => i.ChildItems, i => results.Add(i.Index));

            CollectionAssert.AreEqual(Enumerable.Range(1, 13), results);
        }

        [Test]
        public void ValidateBreadthFirstForEachMultipleRoot()
        {
            var rootTreeNode1 = new TreeNode(1,
                new List<TreeNode>()
                {
                    new TreeNode(2,
                        new List<TreeNode>()
                        {
                            new TreeNode(4),
                            new TreeNode(5),
                            new TreeNode(6)
                        }),
                    new TreeNode(3,
                        new List<TreeNode>()
                        {
                            new TreeNode(7),
                            new TreeNode(8),
                            new TreeNode(9)
                        }),
                });

            var rootTreeNode2 = new TreeNode(10,
                new List<TreeNode>()
                {
                    new TreeNode(11,
                        new List<TreeNode>()
                        {
                            new TreeNode(13),
                            new TreeNode(14),
                            new TreeNode(15)
                        }),
                    new TreeNode(12,
                        new List<TreeNode>()
                        {
                            new TreeNode(16),
                            new TreeNode(17),
                            new TreeNode(18)
                        }),
                });

            var results = new List<int>();
            var rootTreeNodes = new List<TreeNode>() { rootTreeNode1, rootTreeNode2 };
            rootTreeNodes.ForEach(TraversalType.BreadthFirst, i => i.ChildItems, i => results.Add(i.Index));

            CollectionAssert.AreEqual(Enumerable.Range(1, 18), results);
        }
    }
}
