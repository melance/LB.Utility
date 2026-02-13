using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB.Utility.Extensions
{
    public static class UnitConversionExtensions
    {
        public static String InchesToFeet(this Int32 inches, Boolean shortFormat)
        {
            var feet = (Int32)Math.Floor((Double)inches / 12);
            var remainder = inches % 12;
            if (shortFormat)
            {
                if (feet == 0) return inches.ToString("#0\"");
                if (remainder == 0) return feet.ToString("#,##0'");
                return $"{feet:#,##0}'{remainder}\"";
            }
            else
            {
                var footWord = feet == 1 ? "foot" : "feet";
                var inchWord = remainder == 1 ? "inch" : "inches";
                if (feet == 0) return inches.ToString($"#0 {inchWord}");
                if (remainder == 0) return feet.ToString($"#,##0 {footWord}");
                return $"{feet:#,##0} {footWord} {remainder:#0} {inchWord}";
            }
        }

        public static Double InchesToCentimeters(this Int32 inches) => inches * 2.54;
    }
}
