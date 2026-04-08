using LB.Utility.Types;

namespace LB.Utility.Test;

[TestClass]
public sealed class FractionTests
{
    public TestContext TestContext { get; set; }

    [TestMethod]
    [DataRow(0.5, 1, 2)]
    [DataRow(0.25, 1, 4)]
    [DataRow(0.75, 3, 4)]
    public void SimpleFractions(Double value, Int32 n, Int32 d)
    {
        var fraction = new Fraction(value);
        Assert.AreEqual(n, fraction.Numerator);
        Assert.AreEqual(d, fraction.Denominator);
    }

    [TestMethod]
    [DataRow(0.3, 3, 10)]
    [DataRow(0.4, 2, 5)]
    public void ComplexFractions(Double value, Int32 n, Int32 d)
    {
        var fraction = new Fraction(value);
        TestContext.WriteLine(fraction.ToString());
        Assert.AreEqual(n, fraction.Numerator);
        Assert.AreEqual(d, fraction.Denominator);
    }

    [TestMethod]
    public void AddFractions()
    {
        var a = new Fraction(0, 1, 2);
        var b = new Fraction(0, 1, 4);
        var c = a + b;
        Assert.AreEqual(new Fraction(0, 3, 4), c);
    }

    [TestMethod]
    public void SubtractFractions()
    {
        var a = new Fraction(0, 1, 2);
        var b = new Fraction(0, 1, 4);
        var c = a - b;
        Assert.AreEqual(new Fraction(0, 1, 4), c);
    }
}