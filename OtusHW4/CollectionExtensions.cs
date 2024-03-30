public static class CollectionExtensions
{
    public static T GetMax<T>(this IEnumerable<T> collection, Func<T, float> convertToNumber) where T : class
    {
        T maxItem = null;
        float maxValue = float.MinValue;

        foreach (var item in collection)
        {
            float value = convertToNumber(item);
            if (value > maxValue)
            {
                maxValue = value;
                maxItem = item;
            }
        }

        return maxItem;
    }
}
