namespace LB.Utility.Extensions;

/// <summary>
/// Extension methods for Collections
/// </summary>
public static class CollectionExtensions
{
    /// <summary>
    /// Trim all values of whitespace
    /// </summary>
    /// <param name="extended">The collection to loop through</param>
    /// <returns>The collection with whitespace trimmed from each element</returns>
    public static IEnumerable<String> TrimAll(this IEnumerable<String> extended)
    {
        foreach (var item in extended)
        {
            yield return item.Trim();
        }
    }

    /// <summary>
    /// Returns all non null values from the collection
    /// </summary>
    /// <typeparam name="T">The type of the IEnumerable</typeparam>
    /// <param name="extended">The IEnumerable to process</param>
    /// <returns>A IEnumerable with all null values removed</returns>        
    public static IEnumerable<T> NotNull<T>(this IEnumerable<T?> extended)
    {
        foreach (var item in extended)
            if (item != null)
                yield return item;
    }

    /// <summary>
    /// Returns all values that are not null, empty, or whitespace.
    /// </summary>
    /// <param name="extended">The collection to search</param>
    /// <returns>A list excluding null, empty, or whitespace elements</returns>
    public static IEnumerable<String> NotNullOrWhitespace(this IEnumerable<String?> extended)
    {
        foreach (var item in extended)
            if (!String.IsNullOrWhiteSpace(item)) yield return item;
    }

    /// <summary>
    /// Returns true if the collection is null or has no items
    /// </summary>
    /// <typeparam name="T">The type of the IEnumerable</typeparam>
    /// <param name="extended">The list to check</param>
    /// <returns>True if the list is null or has no elements</returns>
    public static Boolean IsNullOrEmpty<T>(this IEnumerable<T?> extended)
    {
        return extended == null || !extended.Any();
    }

    /// <summary>
    /// Divides the list into even parts
    /// </summary>
    /// <typeparam name="T">The type of the IEnumerable</typeparam>
    /// <param name="extended">The list to split</param>
    /// <remarks>For odd length lists, the first half will have on more element</remarks>
    /// <returns>Two lists created from dividing the list in half</returns>
    public static IEnumerable<IEnumerable<T>> GetHalves<T>(this IEnumerable<T> extended)
    {
        var total = extended.Count();
        var parts = new List<List<T>>()
        {
            extended.Take((Int32)Math.Ceiling((Double)total / 2)).ToList(),
            extended.Skip((Int32)Math.Ceiling((Double)total / 2)).ToList()
        };
        return parts;
    }

    /// <summary>
    /// Gets the value associated with the key or returns default value
    /// </summary>
    /// <typeparam name="K">The key type</typeparam>
    /// <typeparam name="V">The value type</typeparam>
    /// <param name="extended">The dictionary to look through</param>
    /// <param name="key">The key to look for</param>
    /// <param name="defaultValue">The default value to return if the key does not exist</param>
    /// <returns>The value from the dictionary if found; otherwise <paramref name="defaultValue"/></returns>
    public static V GetValue<K,V> (this IDictionary<K,V> extended, K key, V defaultValue)
    {
        if (extended.TryGetValue(key, out var v)) return v;
        return defaultValue;
    }
}
