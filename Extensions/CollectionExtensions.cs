using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB.Utility.Extensions
{
    public static class CollectionExtensions
    {
        /// <summary>
        /// Trim all values of whitespace
        /// </summary>
        /// <param name="extended">The collection to loop through</param>
        /// <returns>The collection with whitespace trimmed from each element</returns>
        public static IEnumerable<string> TrimAll(this IEnumerable<string> extended)
        {
            foreach (var item in extended)
            {
                yield return item.Trim();
            }
        }

        /// <summary>
        /// Returns all non null values from the collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="extended"></param>
        /// <returns></returns>        
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
        /// <returns></returns>
        public static IEnumerable<string> NotNullOrWhitespace(this IEnumerable<string?> extended)
        {
            foreach (var item in extended)
                if (!string.IsNullOrWhiteSpace(item)) yield return item;
        }

        /// <summary>
        /// Returns true if the collection is null or has no items
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="extended"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T?> extended)
        {
            return extended == null || !extended.Any();
        }

        /// <summary>
        /// Divides the list into even parts
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="extended"></param>
        /// <param name="divisor"></param>
        /// <returns></returns>
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
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="extended"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static V GetValue<K,V> (this IDictionary<K,V> extended, K key, V defaultValue)
        {
            if (extended.TryGetValue(key, out var v)) return v;
            return defaultValue;
        }
    }
}
