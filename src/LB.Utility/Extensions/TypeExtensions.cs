namespace LB.Utility.Extensions;

/// <summary>
/// Extension methods for the <see cref="Type"/> data type
/// </summary>
public static class TypeExtensions
{
    /// <summary>
    /// Returns true if the type is numeric
    /// </summary>
    public static bool IsNumeric(this Type type)
    {
        return Type.GetTypeCode(type) switch
        {
            TypeCode.Byte or TypeCode.SByte or TypeCode.UInt16 or TypeCode.UInt32 or TypeCode.UInt64 or TypeCode.Int16 or TypeCode.Int32 or TypeCode.Int64 or TypeCode.Decimal or TypeCode.Double or TypeCode.Single => true,
            _ => false,
        };
    }
}
