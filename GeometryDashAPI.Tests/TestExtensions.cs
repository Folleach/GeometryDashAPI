using System.Collections.Generic;

namespace GeometryDashAPI.Tests;

public static class TestExtensions
{
    public static IEnumerable<KeyValuePair<T, T>> Pairs<T>(this IEnumerable<T> source)
    {
        var first = false;
        T firstItem = default;
        foreach (var item in source)
        {
            first = !first;
            if (first)
                firstItem = item;
            else
                yield return new KeyValuePair<T, T>(firstItem, item);
        }

        // ReSharper disable once ConditionIsAlwaysTrueOrFalse
        if (first)
            yield return new KeyValuePair<T, T>(firstItem, default);
    }
}
