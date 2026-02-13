using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB.Utility.Extensions
{
    public static class GenericsExtensions
    {
        /// <summary>
        /// Checks to see if the extended item is in the list
        /// </summary>
        /// <typeparam name="T">The type of the items</typeparam>
        /// <param name="extended">The item to look for in the <paramref name="list"/></param>
        /// <param name="list">The list of items to look in for the <paramref name="extended"/></param>
        /// <returns>True if the <paramref name="extended"/> is in the <paramref name="list"/></returns>
        public static bool In<T>(this T extended, params T[] list)
        {
            if (extended == null) return false;
            foreach (var item in list)
            {
                if (extended.Equals(item)) return true;
            }
            return false;
        }

        /// <summary>
        /// Checks to see if the extended item is in the list using the provided comparer
        /// </summary>
        /// <typeparam name="T">The type of the items</typeparam>
        /// <param name="extended">The item to look for in the <paramref name="list"/></param>
        /// <param name="comparer">The comparer to use when checking for equality</param>
        /// <param name="list">The list of items to look in for the <paramref name="extended"/></param>
        /// <returns>True if the <paramref name="extended"/> is in the <paramref name="list"/></returns>
        public static bool In<T>(this T extended, IEqualityComparer<T> comparer, params T[] list)
        {
            if (extended == null) return false;
            if (comparer == null) return extended.In(list);
            foreach (var item in list)
            {
                if (comparer.Equals(extended, item)) return true;
            }
            return false;
        }

        /// <summary>
        /// Checks to see if the extended item is in the list using the provided comparer
        /// </summary>
        /// <typeparam name="T">The type of the items</typeparam>
        /// <param name="extended">The item to look for in the <paramref name="list"/></param>
        /// <param name="comparer">A function to compare the items</param>
        /// <param name="list">The list of items to look in for the <paramref name="extended"/></param>
        /// <returns>True if the <paramref name="extended"/> is in the <paramref name="list"/></returns>
        public static bool In<T>(this T extended, Func<T, T, bool> comparer, params T[] list)
        {
            if (extended == null) return false;
            if (comparer == null) return extended.In(list);
            foreach (var item in list)
            {
                if (comparer.Invoke(extended, item)) return true;
            }
            return false;
        }

        public static T GetValueOrDefault<T>(this T? extended, T defaultValue)
        {
            if (extended == null) return defaultValue;
            return extended;
        }
    }
}
