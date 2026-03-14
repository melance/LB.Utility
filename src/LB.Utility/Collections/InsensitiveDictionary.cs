namespace LB.Utility.Collections;

/// <summary>
/// A case insensitive Dictionary&lt;String, T&gt;
/// </summary>
public class InsensitiveDictionary<T> : Dictionary<String, T>
{
    /// <summary>
    /// Constructs an InsensitiveDictionary
    /// </summary>
    public InsensitiveDictionary() : base(StringComparer.OrdinalIgnoreCase) { }

    /// <summary>
    /// Add a new item to the dictionary from the provided <see cref="KeyValuePair{String, T}"/>
    /// </summary>
    /// <param name="item">The KeyValuePair to add</param>
    public void Add(KeyValuePair<String, T> item)
    {
        Add(item.Key, item.Value);
    }
}

