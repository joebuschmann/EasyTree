using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace HierarchyHelper.Tests
{
    [TestFixture]
    public class HierarchyExtensionsFixture
    {
        [Test]
        public void ValidateDepthFirstTraversal()
        {
            var rootHierarchyItem = new HierarchyItem(1,
                new List<HierarchyItem>()
                {
                    new HierarchyItem(2,
                        new List<HierarchyItem>()
                        {
                            new HierarchyItem(3),
                            new HierarchyItem(4),
                            new HierarchyItem(5)
                        }),
                    new HierarchyItem(6,
                        new List<HierarchyItem>()
                        {
                            new HierarchyItem(7),
                            new HierarchyItem(8),
                            new HierarchyItem(9)
                        }),
                    new HierarchyItem(10,
                        new List<HierarchyItem>()
                        {
                            new HierarchyItem(11),
                            new HierarchyItem(12),
                            new HierarchyItem(13)
                        }),
                });

            var hierarchyItems = rootHierarchyItem.ToEnumerable(i => i.ChildItems);

            Assert.That(hierarchyItems.Count(), Is.EqualTo(13));

            int index = 1;

            foreach (var hierarchyItem in hierarchyItems)
                Assert.That(hierarchyItem.Index, Is.EqualTo(index++));
        }

        [Test]
        public void ValidateBreadthFirstTraversal()
        {
            var rootHierarchyItem = new HierarchyItem(1,
                new List<HierarchyItem>()
                {
                    new HierarchyItem(2,
                        new List<HierarchyItem>()
                        {
                            new HierarchyItem(5),
                            new HierarchyItem(6),
                            new HierarchyItem(7)
                        }),
                    new HierarchyItem(3,
                        new List<HierarchyItem>()
                        {
                            new HierarchyItem(8),
                            new HierarchyItem(9),
                            new HierarchyItem(10)
                        }),
                    new HierarchyItem(4,
                        new List<HierarchyItem>()
                        {
                            new HierarchyItem(11),
                            new HierarchyItem(12),
                            new HierarchyItem(13)
                        }),
                });

            var hierarchyItems = rootHierarchyItem.ToEnumerable(TraversalType.BreadthFirst, i => i.ChildItems);

            Assert.That(hierarchyItems.Count(), Is.EqualTo(13));

            int index = 1;

            foreach (var hierarchyItem in hierarchyItems)
                Assert.That(hierarchyItem.Index, Is.EqualTo(index++));
        }

        private class HierarchyItem
        {
            private readonly int _index;
            private readonly IEnumerable<HierarchyItem> _childItems;

            public HierarchyItem(int index, IEnumerable<HierarchyItem> childItems = null)
            {
                _index = index;
                _childItems = childItems == null ? Enumerable.Empty<HierarchyItem>() : childItems.ToList();
            }

            public int Index
            {
                get { return _index; }
            }

            public IEnumerable<HierarchyItem> ChildItems
            {
                get { return _childItems; }
            }
        }
    }
}
