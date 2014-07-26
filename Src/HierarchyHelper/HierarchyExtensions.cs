using System;
using System.Collections.Generic;
using System.Linq;
using HierarchyHelper;

// ReSharper disable once CheckNamespace
public static class HierarchyExtensions
{
    public static IEnumerable<T> ToEnumerable<T>(this IEnumerable<T> hierarchyItems, TraversalType traversalType, Func<T, IEnumerable<T>> getChildrenFunc)
    {
        return hierarchyItems.SelectMany(i => i.ToEnumerable(traversalType, getChildrenFunc));
    }

    public static IEnumerable<T> ToEnumerable<T>(this IEnumerable<T> hierarchyItems, Func<T, IEnumerable<T>> getChildrenFunc)
    {
        return hierarchyItems.SelectMany(i => i.ToEnumerable(getChildrenFunc));
    }

    public static IEnumerable<T> ToEnumerable<T>(this T hierarchyItem, TraversalType traversalType, Func<T, IEnumerable<T>> getChildrenFunc)
    {
        var strategy = GetTraversalStrategy(traversalType, getChildrenFunc);
        strategy.AddItem(hierarchyItem);

        while (strategy.HasMoreItems)
        {
            var currentItem = strategy.GetNextItem();
            yield return currentItem;

            foreach (var childItem in strategy.GetChildren(currentItem))
                strategy.AddItem(childItem);
        }
    }

    public static IEnumerable<T> ToEnumerable<T>(this T hierarchyItem, Func<T, IEnumerable<T>> getChildrenFunc)
    {
        return hierarchyItem.ToEnumerable(TraversalType.DepthFirst, getChildrenFunc);
    }

    public static void ForEach<T>(this IEnumerable<T> hierarchyItems, TraversalType traversalType, Func<T, IEnumerable<T>> getChildrenFunc, Action<T> action)
    {
        foreach (var hierarchyItem in hierarchyItems.ToEnumerable(traversalType, getChildrenFunc))
            action(hierarchyItem);
    }

    public static void ForEach<T>(this IEnumerable<T> hierarchyItems, Func<T, IEnumerable<T>> getChildrenFunc, Action<T> action)
    {
        foreach (var hierarchyItem in hierarchyItems.ToEnumerable(getChildrenFunc))
            action(hierarchyItem);
    }

    public static void ForEach<T>(this T hierarchyItem, TraversalType traversalType, Func<T, IEnumerable<T>> getChildrenFunc, Action<T> action)
    {
        foreach (var item in hierarchyItem.ToEnumerable(traversalType, getChildrenFunc))
            action(item);
    }

    public static void ForEach<T>(this T hierarchyItem, Func<T, IEnumerable<T>> getChildrenFunc, Action<T> action)
    {
        foreach (var item in hierarchyItem.ToEnumerable(getChildrenFunc))
            action(item);
    }

    private static ITraversalStrategy<T> GetTraversalStrategy<T>(TraversalType traversalType, Func<T, IEnumerable<T>> getChildrenFunc)
    {
        switch (traversalType)
        {
            case TraversalType.BreadthFirst:
                return new BreadthFirstStrategy<T>(getChildrenFunc);

            default:
                return new DepthFirstStrategy<T>(getChildrenFunc);

        }
    }
}

public enum TraversalType
{
    DepthFirst,
    BreadthFirst
}
