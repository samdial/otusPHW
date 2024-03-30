public static class EnumerableExtensions
{
    public static TElement GetMax<TElement, TKey>(this IEnumerable<TElement> sourceCollection,
                                                  Func<TElement, TKey> keySelector)
        where TKey : IComparable<TKey>
    {
        if (sourceCollection == null)
            throw new ArgumentNullException(nameof(sourceCollection));
        if (keySelector == null)
            throw new ArgumentNullException(nameof(keySelector));

        if (!sourceCollection.Any())
            throw new ArgumentException("Collection cannot be empty.");

        TElement max = sourceCollection.First();
        TKey maxKey = keySelector(max);
        foreach (TElement elem in sourceCollection)
        {
            TKey currentKey = keySelector(elem);
            if (currentKey.CompareTo(maxKey) > 0)
            {
                max = elem;
                maxKey = currentKey;
            }
        }
        return max;
    }
}
