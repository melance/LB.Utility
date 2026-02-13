using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LB.Utility.Extensions;
using Markdig.Helpers;

namespace LB.Utility.Extensions
{
    public static class StringExtensions
	{
		/// <summary>
		/// Determines if the indeterminite article before the string should be A or An
		/// </summary>
		/// <param name="value">The string to evaluate</param>
		/// <returns>The <paramref name="value"/> prefixed with an "a" or "an".</returns>
		/// <remarks>Only works with English</remarks>
		public static string AOrAn(this string value)
		{
			if (string.IsNullOrWhiteSpace(value)) return value;
			if (value[0].IsVowel()) return $"an {value}";
			return $"a {value}";
		}

		/// <summary>
		/// Returns an empty string if extended is null
		/// </summary>
		/// <param name="extended">The string to evaluate</param>
		/// <returns>If the string is null, an <see cref="String.Empty"/>; otherwise, <paramref name="extended"/></returns>
		public static String GetValueOrDefault(this String? extended)
		{
			if (extended == null) return String.Empty;
			return extended.ToString();
		}

		/// <summary>
		/// Returns the defaultValue if extended is null
		/// </summary>
		/// <param name="extended">The string to evaluate</param>
		/// <param name="defaultValue">The value to return if <pararef name="extended" /> is null</param>
		/// <returns>If the string is null, an <see cref="String.Empty"/>; otherwise, <paramref name="defaultValue"/></returns>
		public static String GetValueOrDefault(this String? extended, String defaultValue)
		{
			if (extended == null) return defaultValue;
			return extended.ToString();
		}

        /// <summary>
        /// Indicates whether a specified string is null or empty.
        /// </summary>
        /// <param name="extended">The string to evaluate</param>
        /// <returns>true if the value parameter is null or <see cref="String.Empty"/>.</returns>
        public static Boolean IsNullOrEmpty([NotNullWhen(false)] this String? extended)
		{
			return String.IsNullOrEmpty(extended);
		}

		/// <summary>
		/// Returns the defaultvalue if extended is null or empty string
		/// </summary>		
		/// <param name="extended">The string to evaluate</param>
		/// <param name="defaultValue">The value to return if <pararef name="extended" /> is null</param>
		/// <returns>If the string is null or equal to <see cref="String.Empty"/> <paramref name="defaultValue"/>; otherwise, <paramref name="extended"/></returns>
		public static String IsNullOrEmpty([NotNullWhen(false)] this String? extended, String defaultValue)
		{
			if (String.IsNullOrEmpty(extended)) return defaultValue;
			return extended;
		}

		/// <summary>
		/// Returns the defaultvalue if extended is null or whitespace
		/// </summary>		
		/// <param name="extended">The string to evaluate</param>
		/// <param name="defaultValue">The value to return if <pararef name="extended" /> is null</param>
		/// <returns>If the string is null, <see cref="String.Empty"/>, or consists exclusively of white-space characters <paramref name="defaultValue"/>; otherwise, <paramref name="extended"/></returns>
		public static String IsNullOrWhitespace([NotNullWhen(false)] this String? extended, String defaultValue)
        {
			if (String.IsNullOrWhiteSpace(extended)) return defaultValue;
			return extended;
		}

		/// <summary>
		/// Indicates whether a specified string is null, empty, or consists only of white-space characters.
		/// </summary>
		/// <param name="extended">The string to evaluate</param>
		/// <returns>true if the value parameter is null or <see cref="String.Empty"/>, or if value consists exclusively of white-space characters.</returns>
		public static Boolean IsNullOrWhitespace([NotNullWhen(false)] this String? extended)
		{
			return String.IsNullOrWhiteSpace(extended);
		}

		/// <summary>
		/// Removes the listed items from the string
		/// </summary>
		/// <param name="extended"></param>
		/// <param name="items"></param>
		/// <returns></returns>
		public static String RemoveAll(this String extended, params String[] items)
		{			
			var value = extended;
			foreach(var item in items)
			{
				value = value.Replace(item, String.Empty);
			}
			return value;
		}

        /// <summary>
        /// Removes the listed items from the string
        /// </summary>
        /// <param name="extended"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public static String RemoveAll(this String extended, params char[] items)
        {
            var value = extended;
            foreach (var item in items)
            {
                value = value.Replace(item.ToString(), String.Empty);
            }
            return value;
        }

        /// <summary>
        /// Remove articles from the beginning of strings
        /// </summary>
        /// <param name="extended">The string to remove articles from</param>
        /// <returns>The string minus any articles found at the beginning</returns>
        public static String RemoveArticles(this String extended)
		{
			if (extended.StartsWith("a ", StringComparison.InvariantCultureIgnoreCase)) return extended[2..];
			if (extended.StartsWith("an ", StringComparison.InvariantCultureIgnoreCase)) return extended[3..];
			if (extended.StartsWith("the ", StringComparison.InvariantCultureIgnoreCase)) return extended[4..];
			return extended;
		}
        /// <summary>
        /// Removes the first instance of value in the string.  If not found returns the original string.
        /// </summary>
        /// <param name="extended">The string to manipulate</param>
        /// <param name="value">The value to find</param>
        /// <returns>The <paramref name="extended"/> sans the first instance of <paramref name="value"/></returns>
        public static String RemoveFirst(this String extended, String value)
		{
			var index = extended.IndexOf(value);
			if (index >= 0)
				return extended.Remove(index, value.Length);
			return extended;
		}

		/// <summary>
		/// Removes the last instance of value in the string.  If not found returns the original string.
		/// </summary>
		/// <param name="extended">The string to manipulate</param>
		/// <param name="value">The value to find</param>
		/// <returns>The <paramref name="extended"/> sans the last instance of <paramref name="value"/></returns>
		public static String RemoveLast(this String extended, String value) 
		{
            var index = extended.LastIndexOf(value);
            if (index >= 0)
                return extended.Remove(index, value.Length);
            return extended;
        }

		/// <summary>
		/// Returns the first <paramref name="characters"/> characters of the right portion of a string
		/// </summary>
		/// <param name="extended">The string to manipulate</param>
		/// <param name="characters">The number of characters to select</param>
		/// <returns>A substring of <paramref name="extended"/></returns>
		/// <remarks>if <paramref name="characters"/> is greater than the length of <paramref name="extended"/>, <paramref name="extended"/> is returned unchanged.</remarks>
		public static String Right(this String extended, Int32 characters)
		{
			if (extended.Length < characters) return extended;
			return extended[^characters..];
		}

		/// <summary>
		/// Splits a string into evenly sized parts
		/// </summary>
		/// <param name="extended">The string to split</param>
		/// <param name="partSize">The number of characters per substring</param>
		/// <returns>A <see cref="List{String}"/> of substrings from <paramref name="extended"/></returns>
		public static List<String>? Split(this String extended, Int32 partSize)
		{
			if (extended == null) return null;
			var parts = new List<String>();
			for (var i = 0; i + partSize <= extended.Length; i += partSize)
			{
				parts.Add(extended[i..(i + partSize)]);
			}
			var mod = extended.Length % partSize;
			if (mod > 0)
			{
				parts.Add(extended[^mod..]);
			}
			return parts;
        }

		public static IEnumerable<string> Split(this string extended, char separator, char escapeCharacter) => extended.Split(separator, escapeCharacter, false);

        public static IEnumerable<string> Split(this string extended, char separator, char escapeCharacter, bool removeEmptyEntries)
        {
            var buffer = new StringBuilder();
            bool escape = false;

            foreach (var c in extended)
            {
                if (!escape && c == separator)
                {
                    if (!removeEmptyEntries || buffer.Length > 0)
                    {
                        yield return buffer.ToString();
                    }

                    buffer.Clear();
                }
                else
                {
                    if (c == escapeCharacter && !escape)
                    {
                        escape = true;
                    }
                    else
                    {
                        buffer.Append(c);
                        escape = false;
                    }
                }
            }

            if (buffer.Length != 0)
            {
                yield return buffer.ToString();
            }
        }

		/// <summary>
		/// Inserts a character between lowercase and uppercase letters
		/// </summary>
		/// <param name="extended"></param>
		/// <returns></returns>
		public static String SplitOnCaps(this String extended, Char replacement = ' ')
		{
            var result = new StringBuilder();
            for (var i = 0; i < extended.Length; i++)
            {
                if (i > 0 && Char.IsUpper(extended[i])) result.Append(replacement);
                result.Append(extended[i]);
            }
            return result.ToString();
        }

		/// <summary>
		/// Converts a string to a valid html id by removing all non-alphanumeric characters
		/// </summary>
		/// <param name="extended">The string to convert</param>
		/// <returns></returns>
        public static String ToHTMLId(this String extended)
        {
			var value = (from c in extended
						where Char.IsLetterOrDigit(c) || Char.IsWhiteSpace(c)
						select Char.IsWhiteSpace(c) ? '-' : c).ToArray();
			return new String(value).ToLower();
        }

		/// <summary>
		/// Converts a string to title case
		/// </summary>
		public static String ToTitleCase(this String? extended)
		{
			if (extended == null) return String.Empty;
			var textInfo = new CultureInfo("en-US", false).TextInfo;
			return textInfo.ToTitleCase(extended);
		}

        /// <summary>
        /// Returns the numbers contained in a string as a numeric value of appropriate type.
        /// </summary>
        /// <param name="extended"></param>
        /// <returns></returns>
        public static Double Val(this String extended)
		{
			if (extended.IsNullOrWhitespace()) return 0;
			var digits = new StringBuilder();
			var i = 0;
			var decimalFound = false;
			var c = extended[0];

			while (c.IsDigit() || (c == '.' && !decimalFound) || c == ',')
			{
				if (c.IsDigit()) digits.Append(c);
				if (c == '.')
				{
					digits.Append(c);
					decimalFound = true;
				}
				i++;
				if (i < extended.Length)
					c = extended[i];
				else
					c = ' ';
			}

			if (Double.TryParse(digits.ToString(), out double result)) return result;
			return 0;
		}
    }
}
