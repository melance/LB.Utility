using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB.Utility.Extensions
{
    public static class EnumExtensions
    {
        public static T? GetAttributeOfType<T>(this Enum value) where T : Attribute
        {
            var type = value.GetType();
            var info = type.GetMember(value.ToString());
            if (info.Length == 0) return null;
            var attributes = info[0].GetCustomAttributes(typeof(T), false);
            return (attributes.Length > 0) ? (T)attributes[0] : null;
        }

        public static String GetDisplayName(this Enum value) => value.GetDisplay()?.Name ?? value.ToString();
        public static String GetDisplayDescription(this Enum value) => value.GetDisplay()?.Description ?? value.ToString();
        public static DisplayAttribute? GetDisplay(this Enum value) => value.GetAttributeOfType<DisplayAttribute>();            
    }
}
