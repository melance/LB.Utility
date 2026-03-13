using System.Globalization;
using System.Text;

namespace LB.Utility.Extensions;

public static class NumericExtensions
{
    #region Members
    /// <summary>
    /// List of Data Sizes for the ToDataSize methods
    /// </summary>
    private static readonly List<(ulong MaxBytes, String Name)> _dataSizes =
    [
        new((ulong)1e3, "b"),
        new((ulong)1e6, "kb"),
        new((ulong)1e9, "mb"),
        new((ulong)1e12, "gb"),
        new((ulong)1e15, "tb"),
        new((ulong)1e18, "pb")
    ];

    /// <summary>
    /// List of Roman Numerals for the <see cref="ToRomanNumerals(Int32)"/> method
    /// </summary>
    private static readonly List<Tuple<Int32, String>> _romanNumerals = new()
    {
        { Tuple.Create(1000, "M") },
        { Tuple.Create(900, "CM") },
        { Tuple.Create(500, "D") },
        { Tuple.Create(400, "CD") },
        { Tuple.Create(100, "C") },
        { Tuple.Create(90, "XC") },
        { Tuple.Create(50, "L") },
        { Tuple.Create(40, "XL") },
        { Tuple.Create(10, "X") },
        { Tuple.Create(9,"IX") },
        { Tuple.Create(5, "V") },
        { Tuple.Create(4, "IV") },
        { Tuple.Create(1, "I") }
    };
    #endregion

    #region Comparisons
    /// <summary>
    /// Checks that the value is between the min and max inclusive.  Returns the value
    /// unless it is outside the range when it will return the min or max as appropriate
    /// </summary>
    /// <param name="extended"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static Int32 Between(this Int32 extended, Int32 min, Int32 max)
    {
        if (extended < min) return min;
        if (extended > max) return max;
        return extended;
    }

    public static Boolean IsEven(this Int32 extended) => extended % 2 == 0;
    public static Boolean IsOdd(this Int32 extended) => !extended.IsEven();
    #endregion

    #region Formatting
    /// <summary>
    /// Returns the value in inches to a string in the format ##'##"
    /// </summary>
    /// <param name="extended"></param>
    /// <returns></returns>
    public static String ToFeetInches(this Int32 extended, String? format = null, IFormatProvider? provider = null) => ToFeetInches((Double)extended, format, provider);

    /// <summary>
    /// Returns the value in inches to a string in the format given
    /// 
    /// f = feet
    /// F = feet even if zero
    /// i = inches
    /// I = inches even if zero
    /// n = total inches
    /// </summary>
    /// <param name="extended"></param>
    /// <returns></returns>
    public static String ToFeetInches(this Double extended, String? format = null, IFormatProvider? provider = null)
    {
        format ??= "f'i\"";
        provider ??= CultureInfo.InvariantCulture;

        Int32 feet = (Int32)Math.Floor(extended / 12d);
        Double inches = extended % 12d;

        String feetText = feet.ToString(provider);
        String inchesText = inches.ToString("0.#", provider);
        String totalInchesText = extended.ToString("0.#", provider);

        var builder = new StringBuilder();
        Boolean escape = false;

        for (Int32 i = 0; i < format.Length; i++)
        {
            char c = format[i];

            if (escape)
            {
                builder.Append(c);
                escape = false;                
            }
            else if (c == '\\')
            {
                escape = true;                
            }
            else
            {
                switch (c)
                {
                    case 'f':
                        if (feet != 0)
                            builder.Append(feetText);
                        break;
                    case 'F':
                        builder.Append(feetText);
                        break;
                    case 'i':
                        if (inches != 0)
                            builder.Append(inchesText);
                        break;
                    case 'I':
                        builder.Append(inchesText);
                        break;
                    case 'n':
                        builder.Append(totalInchesText);
                        break;
                    default:
                        builder.Append(c);
                        break;
                };
            }
        }
        return builder.ToString();
    }

    /// <summary>
    /// Adds the ordinal suffix to an <see cref="Int32"/>
    /// </summary>
    /// <param name="extended">The <see cref="Int32"/> to add the suffix to</param>
    /// <returns>The orginal with it's appropriate suffix</returns>
    /// <remarks>Currently only supports English</remarks>
    public static String ToOrdinal(this Int32 extended) => extended.ToOrdinal(false);


    /// <summary>
    /// Adds the ordinal suffix to an <see cref="Int32"/>
    /// </summary>
    /// <param name="extended">The <see cref="Int32"/> to add the suffix to</param>
    /// <returns>The orginal with it's appropriate suffix</returns>
    /// <remarks>Currently only supports English</remarks>
    public static String ToOrdinal(this Int32 extended, Boolean superscript)
    {
        Int32 lastDigit = Convert.ToInt32(extended.ToString().Right(1));
        String ending;

        if (extended <= 0)
            return extended.ToString();

        ending = lastDigit switch
        {
            1 => "st",
            2 => "nd",
            3 => "rd",
            _ => "th",
        };

        if ((extended % 100).In(11, 12, 13))
            ending = "th";

        return $"{extended}{(superscript ? "<sup>" : "")}{ending}{(superscript ? "</sup>" : "")}";
    }

    /// <summary>
    /// Converts an <see cref="Int32"/> to its numeric word
    /// </summary>
    /// <param name="extended">The <see cref="Int32"/> to convert</param>
    /// <returns>The numeric word for the <see cref="Int32"/></returns>
    /// <remarks>Currently only supports English</remarks>
    public static String ToText(this Int32 extended)
    {
        return ((Int64)extended).ToText();
    }

    /// <summary>
    /// Converts an <see cref="Int32"/> to its numeric word
    /// </summary>
    /// <param name="extended">The <see cref="Int32"/> to convert</param>
    /// <returns>The numeric word for the <see cref="Int32"/></returns>
    /// <remarks>Currently only supports English</remarks>
    public static String ToText(this Int64 extended)
    {
        String ProcessValue(long divisor, String valueDescriptor)
        {
            var value = new StringBuilder();
            var remainder = extended % divisor;

            value.Append($"{(extended / divisor).ToText()} {valueDescriptor}");

            if (remainder != 0) value.Append($" {remainder.ToText()}");

            return value.ToString();
        }

        String value;

        if (extended < 0)
        {
            value = Convert.ToString("negative ") + (-extended).ToText();
        }
        else if (extended == 0)
        {
            value = "zero";
        }
        else if (extended <= 19)
        {
            value = new String[] { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" }[extended - 1];
        }
        else if (extended <= 99)
        {
            var tens = new String[] { "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" }[extended / 10 - 2];
            value = $"{tens}{(extended % 10 == 0 ? String.Empty : $"-{(extended % 10).ToText()}")}";
        }
        else if (extended <= 999)
        {
            value = ProcessValue(100, "hundred");
        }
        else if (extended <= 999999)
        {
            value = ProcessValue(1000, "thousand");
        }
        else if (extended <= 999999999)
        {
            value = ProcessValue(1000000, "million");
        }
        else if (extended <= 999999999999)
        {
            value = ProcessValue(1000000000, "billion");
        }
        else if (extended <= 999999999999999)
        {
            value = ProcessValue(1000000000000, "trillion");
        }
        else if (extended <= 999999999999999999)
        {
            value = ProcessValue(1000000000000000, "quadrillion");
        }
        else
        {
            value = ProcessValue(1000000000000000000, "quintillion");
        }

        return value;
    }

    /// <summary>
    /// Returns the Roman Numeral equivalent of the provided integer
    /// </summary>
    /// <remarks>
    /// Roman numerals only support numbers between 1 and 4000.  Numbers outside of this range are returned as Arabic numbers.
    /// </remarks>
    public static String ToRomanNumerals(this Int32 extended)
    {
        var value = new StringBuilder();
        var i = 0;
        var number = extended;

        if (extended < 1 || extended > 4000) return extended.ToString();

        while (number > 0 && i < _romanNumerals.Count)
        {
            var currentSymbol = _romanNumerals[i];
            if (currentSymbol.Item1 <= number)
            {
                value.Append(currentSymbol.Item2);
                number -= currentSymbol.Item1;
            }
            else
                i++;
        }

        return value.ToString();
    }

    /// <summary>
    /// Returns the integer as a data size String
    /// </summary>
    /// <param name="extended">The integer to format</param>
    /// <returns>The integer as a data size String</returns>
    /// <example>12345 => 1.23kb</example>
    /// <exception cref="ArgumentException">Thrown if the value is a negative number</exception>
    public static String ToDataSize(this Int32 extended)
    {
        if (extended < 0) throw new ArgumentException("The value must be a positive value.", nameof(extended));
        return ((ulong)extended).ToDataSize();
    }

    public static String ToDataSize(this Int64 extended)
    {
        return ((UInt64)extended).ToDataSize();
    }

    public static String ToDataSize(this Int64 extended, String format)
    {
        return ((UInt64)extended).ToDataSize(format);
    }

    /// <summary>
    /// Returns the integer as a data size String
    /// </summary>
    /// <param name="extended">The integer to format</param>
    /// <returns>The integer as a data size String</returns>
    /// <example>12345 => 1.23kb</example>
    public static String ToDataSize(this ulong extended, String format = "{0}{1}")
    {
        var minBytes = 1d;

        foreach (var (MaxBytes, Name) in _dataSizes)
        {
            if (extended < MaxBytes)
            {
                var result = Math.Round(extended / minBytes, 2);
                return String.Format(format, result, Name);
            }
            minBytes = MaxBytes;
        }
        throw new ArgumentException("Value out of range.", nameof(extended));
    } 
    #endregion
}
