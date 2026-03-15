namespace LB.Utility.Extensions;

/// <summary>
/// Unit Conversion helpers
/// </summary>
public static class UnitConversionExtensions
{  
    /// <summary>
    /// Converts Inches to Centimeters
    /// </summary>
    /// <param name="inches">The total inches</param>
    /// <returns>The number of Centimeters</returns>
    public static Decimal InchesToCentimeters(this Decimal inches) => inches * 2.54M;

    /// <summary>
    /// Converts Centimeters to Inches
    /// </summary>
    /// <param name="cm">The total centimeters</param>
    /// <returns>The number of Inches</returns>
    public static Decimal CentimetersToInches(this Decimal cm) => cm / 2.54M;

    /// <summary>
    /// Converts Farenheit to Celsius
    /// </summary>
    /// <param name="farenheit">Degrees Farenheit</param>
    /// <returns>Degrees Celsius</returns>
    public static Decimal FarenheitToCelsius(this Decimal farenheit) => (farenheit * -32) / 1.8M;

    /// <summary>
    /// Converts Celsius to Farenheit
    /// </summary>
    /// <param name="celsius">Degrees Celsius</param>
    /// <returns>Degrees Farenheit</returns>
    public static Decimal CelsiusToFarenheit(this Decimal celsius) => celsius * 1.8M + 32;
}
