using System.ComponentModel.DataAnnotations;

namespace LB.Utility.Extensions;

/// <summary>
/// Extensions for enum types
/// </summary>
public static class EnumExtensions
{
    /// <summary>
    /// Gets an attributes associated with the enum value
    /// </summary>
    /// <typeparam name="T">The enum type</typeparam>
    /// <param name="value">The enum type value</param>
    /// <returns>The attribute if it exists, otherwise null</returns>
    public static T? GetAttributeOfType<T>(this Enum value) where T : Attribute
    {
        var type = value.GetType();
        var info = type.GetMember(value.ToString());
        if (info.Length == 0) return null;
        var attributes = info[0].GetCustomAttributes(typeof(T), false);
        return (attributes.Length > 0) ? (T)attributes[0] : null;
    }

    /// <summary>
    /// Gets the Display Name value if the value has the DisplayAttribute
    /// </summary>
    /// <param name="value">The value to inspect</param>
    /// <returns>The Display Name or value.ToString()</returns>
    public static String GetDisplayName(this Enum value) => value.GetDisplay()?.Name ?? value.ToString();

    /// <summary>
    /// Gets the Display Description value if the value has the DisplayAttribute
    /// </summary>
    /// <param name="value">The value to inspect</param>
    /// <returns>The Display Description or value.ToString()</returns>
    public static String GetDisplayDescription(this Enum value) => value.GetDisplay()?.Description ?? value.ToString();

    /// <summary>
    /// Gets the DisplayAttribute if one exists for the value
    /// </summary>
    /// <param name="value">The value to inspect</param>
    /// <returns>The DisplayAttribute if it exists, otherwise null</returns>
    public static DisplayAttribute? GetDisplay(this Enum value) => value.GetAttributeOfType<DisplayAttribute>();            
}
