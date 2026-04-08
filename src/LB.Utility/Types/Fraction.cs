using LB.Utility.Extensions;
using System.Diagnostics.CodeAnalysis;

namespace LB.Utility.Types;

public struct  Fraction
{
    public Fraction(Int32 w, Int32 n, Int32 d) => (WholeNumber, Numerator, Denominator) = (w, n, d);
    public Fraction(Double d)
    {
        var result = DoubleToFraction(d);
        Numerator = result.Numerator;
        Denominator = result.Denominator;
    }

    public Int32 WholeNumber { get; set; }
    public Int32 Numerator { get; set; }
    public Int32 Denominator { get; set; }

    public override Boolean Equals([NotNullWhen(true)] Object? obj)
    {
        if (obj is Fraction f)
        {
            return f.Numerator == Numerator && f.Denominator == Denominator;
        }
        return base.Equals(obj);
    }

    public override String ToString()
    {
        return $"{Numerator}/{Denominator}";
    }

    private static Fraction DoubleToFraction(Double value)
    {
        if (Double.IsNaN(value)) throw new ArgumentException("Fraction does not support NaN.", nameof(value));
        if (Double.IsInfinity(value)) throw new ArgumentException("Fraction does not support Infinity.", nameof(value));
        Int32 wholeNumber = (Int32)Math.Floor(value);
        Double fractionPart = Math.Abs(value - wholeNumber);
        if (wholeNumber == value) return new Fraction(wholeNumber, 0, 0);
        Int32 decimalLength = fractionPart.ToString().Length - 2;
        Int32 numerator = Convert.ToInt32(fractionPart.ToString().Right(decimalLength));
        Int32 denominator = Convert.ToInt32($"1{new String('0', decimalLength)}");
        Int32 gcd = GCD(numerator, denominator);
        numerator /= gcd;
        denominator /= gcd;
        return new Fraction(wholeNumber, numerator, denominator);
    }

    private static Int32 GCD(Int32 numerator, Int32 denominator)
    {
        var d = denominator;
        var n = numerator;
        if (d % n == 1) return 1;
        while (d % n != 0)
        {
            var temp = n;
            n = d % n;
            d = temp;
        }

        return n;
    }

    //// Source - https://stackoverflow.com/a/32903747
    //private static Fraction RealToFraction(Double value, Double accuracy)
    //{
    //    if (Double.IsNaN(value)) throw new ArgumentException("Fraction does not support NaN.", nameof(value));
    //    if (Double.IsInfinity(value)) throw new ArgumentException("Fraction does not support Infinity.", nameof(value));
    //    if (accuracy <= 0.0 || accuracy >= 1.0)
    //        throw new ArgumentOutOfRangeException(nameof(accuracy), "Must be > 0 and < 1.");

    //    Int32 sign = Math.Sign(value);

    //    if (sign == -1)
    //    {
    //        value = Math.Abs(value);
    //    }

    //    // Accuracy is the maximum relative error; convert to absolute maxError
    //    double maxError = sign == 0 ? accuracy : value * accuracy;

    //    Int32 n = (Int32)Math.Floor(value);
    //    value -= n;

    //    if (value < maxError)
    //    {
    //        return new Fraction(sign * n, 1);
    //    }

    //    if (1 - maxError < value)
    //    {
    //        return new Fraction(sign * (n + 1), 1);
    //    }

    //    // The lower fraction is 0/1
    //    Int32 lower_n = 0;
    //    Int32 lower_d = 1;

    //    // The upper fraction is 1/1
    //    Int32 upper_n = 1;
    //    Int32 upper_d = 1;

    //    while (true)
    //    {
    //        // The middle fraction is (lower_n + upper_n) / (lower_d + upper_d)
    //        Int32 middle_n = lower_n + upper_n;
    //        Int32 middle_d = lower_d + upper_d;

    //        if (middle_d * (value + maxError) < middle_n)
    //        {
    //            // real + error < middle : middle is our new upper
    //            upper_n = middle_n;
    //            upper_d = middle_d;
    //        }
    //        else if (middle_n < (value - maxError) * middle_d)
    //        {
    //            // middle < real - error : middle is our new lower
    //            lower_n = middle_n;
    //            lower_d = middle_d;
    //        }
    //        else
    //        {
    //            // Middle is our best fraction
    //            return new Fraction((n * middle_d + middle_n) * sign, middle_d);
    //        }
    //    }
    //}
}