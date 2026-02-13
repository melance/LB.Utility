using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB.Utility
{
    public static class StringBuilderExtensions
    {
		/// <summary>
		/// Returns true if the string builder is empty
		/// </summary>
		/// <param name="extended">The <see cref="StringBuilder"/> to check</param>
		/// <returns>
		/// true if the value parameter is null or an empty string (""); otherwise, false.
		/// </returns>
		public static Boolean IsNullOrEmpty(this StringBuilder extended)
		{
			if (extended == null) return true;
			return String.IsNullOrEmpty(extended.ToString());
		}

		/// <summary>
		/// Returns true if the string builder is empty
		/// </summary>
		/// <param name="extended">The <see cref="StringBuilder"/> to check</param>
		/// <returns>
		/// true if the value parameter is null or System.String.Empty, or if value consists exclusively of white-space characters; otherwise, false.
		/// </returns>
		public static Boolean IsNullOrWhitespace(this StringBuilder extended)
		{
			if (extended == null) return true;
			return String.IsNullOrWhiteSpace(extended.ToString());
		}
	}
}
