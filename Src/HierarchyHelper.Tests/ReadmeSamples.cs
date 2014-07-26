using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace TreeTraversalExtensions.Tests
{
    [TestFixture]
    public class ReadmeSamples
    {
        [Test]
        public void ToEnumerableSample1()
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
        }

        [Test]
        public void ToEnumerableSample2()
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

            // Breadth-first traversal
            var items = rootTreeNode.ToEnumerable(TraversalType.BreadthFirst, n => n.ChildItems);
            CollectionAssert.AreEqual(Enumerable.Range(1, 13), items.Select(n => n.Index));
        }
    }
}
