namespace LB.Utility.Extensions;

/// <summary>
/// Extension for Date data types
/// </summary>
public static class DateExtensions
{
    /// <summary>
    /// The parts of a date time
    /// </summary>
    public enum DatePart
    {
        Millisecond,
        Second,
        Minute,
        Hour,
        Day,
        Month,
        Year
    }

    /// <summary>
    /// Truncates a date to the granularity provided.
    /// </summary>
    /// <param name="extended">The <see cref="DateTime"/> to truncate</param>
    /// <param name="part">The granularity to truncate the date to</param>
    /// <returns>A <see cref="DateTime"/> truncated to the provided granularity</returns>
    public static DateTime Truncate(this DateTime extended, DatePart part)
    {
        return part switch
        {
            DatePart.Second => new DateTime(extended.Year, extended.Month, extended.Day, extended.Hour, extended.Minute, extended.Second),
            DatePart.Minute => new DateTime(extended.Year, extended.Month, extended.Day, extended.Hour, extended.Minute, 0),
            DatePart.Hour => new DateTime(extended.Year, extended.Month, extended.Day, extended.Hour, 0, 0),
            DatePart.Day => extended.Date,
            DatePart.Month => new DateTime(extended.Year, extended.Month, 1),
            DatePart.Year => new DateTime(extended.Year, 1, 1),
            _ => extended,
        };
    }

    /// <summary>
    /// Attempts to convert the object to a date time value
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="extended"></param>
    /// <returns>The object as a date time or null if the conversion fails</returns>
    public static DateTime? ToDateTime<T>(this T extended) => extended.ToDateTime(null);

    /// <summary>
    /// Attempts to convert the object to a date time value
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="extended"></param>
    /// <returns>The object as a date time or <paramref name="defaultValue"/> if the conversion fails</returns>
    public static DateTime? ToDateTime<T>(this T extended, DateTime? defaultValue)
    {
        switch (extended)
        {
            case DateTime dateTime:
                return dateTime;
            case string stringValue:
                if (DateTime.TryParse(stringValue, out var dateTimeValue))
                    return dateTimeValue;
                return defaultValue;
            default:
                var value = extended as DateTime?;
                return value ?? defaultValue;
        }
    }
}
