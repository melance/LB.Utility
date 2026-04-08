using LB.Utility.Extensions;
using System.Diagnostics.CodeAnalysis;

namespace LB.Utility.Types;

public struct Fraction
{
    public Fraction(Int32 w, Int32 n, Int32 d)
    {
        WholeNumber = w;
        Numerator = n;
        Denominator = d;
        Simplify();
    }
    public Fraction(Double d)
    {
        var result = DoubleToFraction(d);
        WholeNumber = result.WholeNumber;
        Numerator = result.Numerator;
        Denominator = result.Denominator;
        Simplify();
    }

    public Int32 WholeNumber { get; set; }
    public Int32 Numerator { get; set; }
    public Int32 Denominator { get; set; }

    private void Simplify()
    {
        var n = Numerator;
        var d = Denominator;

        while (n > d)
        {
            WholeNumber++;
            n -= d;
        }
        Numerator = n;
    }

    public override Boolean Equals([NotNullWhen(true)] Object? obj)
    {
        if (obj is Fraction f)
        {
            return f.WholeNumber == WholeNumber && f.Numerator == Numerator && f.Denominator == Denominator;
        }
        return base.Equals(obj);
    }

    public override String ToString()
    {
        if (Numerator == 0) return WholeNumber.ToString();
        if (WholeNumber == 0) return $"{Numerator}/{Denominator}";
        return $"{WholeNumber} {Numerator}/{Denominator}";
    }

    public String ToString(String? format)
    {
        if (String.IsNullOrWhiteSpace(format)) return ToString();
        var improperNumerator = Denominator == 0
                                ? WholeNumber
                                : Numerator + Denominator * WholeNumber;

        return format.Replace("{W}", WholeNumber.ToString())
                     .Replace("{w}", WholeNumber == 0 ? String.Empty : WholeNumber.ToString())
                     .Replace("{N}", Numerator.ToString())
                     .Replace("{n}", Numerator == 0 ? String.Empty : Numerator.ToString())
                     .Replace("{D}", Denominator.ToString())
                     .Replace("{d}", Denominator == 0 ? String.Empty : Denominator.ToString())
                     .Replace("{I}", improperNumerator.ToString())
                     .Replace("{i}", improperNumerator == 0 ? String.Empty : improperNumerator.ToString())
                     .Replace("{MF}", ToString());
    }

    public static Fraction operator *(Fraction a, Fraction b)
    {
        var dA = (Double)a;
        var dB = (Double)b;
        var dR = dA * dB;
        return new Fraction(dR);
    }

    public static Fraction operator /(Fraction a, Fraction b)
    {
        var dA = (Double)a;
        var dB = (Double)b;
        var dR = dA / dB;
        return new Fraction(dR);
    }

    public static Double operator %(Fraction a, Fraction b)
    {
        var dA = (Double)a;
        var dB = (Double)b;
        return dA % dB;
    }

    public static Fraction operator +(Fraction a, Fraction b)
    {
        var dA = (Double)a;
        var dB = (Double)b;
        var dR = dA + dB;
        return new Fraction(dR);
    }

    public static Fraction operator -(Fraction a, Fraction b)
    {
        var dA = (Double)a;
        var dB = (Double)b;
        var dR = dA - dB;
        return new Fraction(dR);
    }

    public static Boolean operator >(Fraction a, Fraction b)
    {
        var dA = (Double)a;
        var dB = (Double)b;
        return dA > dB;
    }

    public static Boolean operator <(Fraction a, Fraction b)
    {
        var dA = (Double)a;
        var dB = (Double)b;
        return dA < dB;
    }

    public static Boolean operator <=(Fraction a, Fraction b)
    {
        return a < b || a == b;
    }

    public static Boolean operator >=(Fraction a, Fraction b)
    {
        return a > b || a == b;
    }

    public static Boolean operator ==(Fraction a, Fraction b)
    {
        return a.Equals(b);
    }

    public static Boolean operator !=(Fraction a, Fraction b)
    {
        return !a.Equals(b);
    }

    public static explicit operator Double(Fraction a)
    {
        return a.WholeNumber + ((Double)a.Numerator / a.Denominator);
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

    public static Int32 GCD(Int32 numerator, Int32 denominator)
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

    public static Int32 LCM(Int32 a, Int32 b)
    {
        var gcd = GCD(a, b);
        return a / gcd * b; 
    }
}