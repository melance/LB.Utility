using System.Collections.ObjectModel;
using System.Reflection;

namespace LB.Utility.Attributes;

/// <summary>
/// Used to decorate an enumerator value with an alias that the value
/// can be selected by
/// </summary>
[AttributeUsage(AttributeTargets.Field)]
public class EnumAliasAttribute : Attribute
{
    /// <summary>
    /// Will attempt to get the value of the enum by the name or alias
    /// </summary>
    /// <typeparam name="T">The enumerator type</typeparam>
    /// <param name="value">The character value of the enum or its alias</param>
    /// <returns>The enum value if it exists, otherwise null</returns>
    public static T? GetEnumValue<T>(Char value) where T : Enum
    {
        return GetEnumValue<T>(value.ToString());
    }

    /// <summary>
    /// Will attempt to get the value of the enum by the name or alias
    /// </summary>
    /// <typeparam name="T">The enumerator type</typeparam>
    /// <param name="value">The string value of the enum or its alias</param>
    /// <returns>The enum value if it exists, otherwise null</returns>
    public static T? GetEnumValue<T>(String value) where T : Enum
    {
        var type = typeof(T);
        var enumValues = type.GetEnumValues();

        Enum.TryParse(typeof(T), value, out var result);

        if (result == null)
        {
            foreach (var enumValue in enumValues.Cast<T>())
            {
                var info = type.GetMember(enumValue.ToString()).First();
                var aliases = info.GetCustomAttributes<EnumAliasAttribute>();

                foreach (var alias in aliases)
                {
                    if (alias.Aliases.Contains(value, StringComparer.InvariantCultureIgnoreCase))
                        return enumValue;
                }
            }
        }

        return result == null ? default : (T)result;
    }

    /// <summary>
    /// The list of aliases to know the enum by
    /// </summary>
    public ReadOnlyCollection<String> Aliases { get; }

    /// <summary>
    /// Constructs the EnumAliasAttribute with the provided alias
    /// </summary>
    /// <param name="alias">The alias to know the enum value by</param>
    public EnumAliasAttribute(String alias) => Aliases = new ReadOnlyCollection<String>(new String[] { alias });

    /// <summary>
    /// Constructs the EnumAliasAttribute with the provided aliases
    /// </summary>
    /// <param name="aliases">A list of aliases to know the enum value by</param>
    public EnumAliasAttribute(params String[] aliases) => Aliases = new ReadOnlyCollection<String>(aliases);
}
