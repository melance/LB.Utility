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
    public static Double InchesToCentimeters(this Int32 inches) => inches * 2.54;
}
