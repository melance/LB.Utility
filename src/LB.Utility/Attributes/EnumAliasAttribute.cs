using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LB.Utility.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class EnumAliasAttribute : Attribute
    {
        public static T? GetEnumValue<T>(Char value) where T : Enum
        {
            return GetEnumValue<T>(value.ToString());
        }

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

        public ReadOnlyCollection<String> Aliases { get; }

        public EnumAliasAttribute(String alias) => Aliases = new ReadOnlyCollection<String>(new String[] { alias });
        public EnumAliasAttribute(params String[] aliases) => Aliases = new ReadOnlyCollection<String>(aliases);
    }
}
