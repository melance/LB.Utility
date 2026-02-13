using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB.Utility.Extensions
{
    public static class CharacterExtensions
    {
        /// <summary>
        /// Determines if the character is a vowel
        /// </summary>
        /// <param name="extended">The character to check</param>
        /// <returns>True if the character is a vowel</returns>
        /// <remarks>Only works with English</remarks>
        public static bool IsVowel(this char extended)
        {
            if ("aeiouAEIOU".Contains(extended)) return true;
            return false;
        }
    }
}
