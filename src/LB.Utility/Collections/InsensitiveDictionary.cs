namespace LB.Utility.Collections;

public class InsensitiveDictionary<T> : Dictionary<String, T>
{
    public InsensitiveDictionary() : base(StringComparer.OrdinalIgnoreCase) { }
    public void Add(KeyValuePair<String, T> item)
    {
        Add(item.Key, item.Value);
    }
}

