namespace LB.Utility.Extensions;

/// <summary>
/// Extensions for the Char data type
/// </summary>
public static class CharacterExtensions
{
    /// <summary>
    /// Determines if the character is a vowel in English
    /// </summary>
    /// <param name="extended">The character to check</param>
    /// <returns>True if the character is a vowel</returns>
    /// <remarks>Only works with English</remarks>
    public static Boolean IsVowel(this Char extended)
    {
        if ("aeiouAEIOU".Contains(extended)) return true;
        return false;
    }
}
