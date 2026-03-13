using LB.Utility.Extensions;

namespace LB.Utility.Test;

[TestClass]
public sealed class StringExtensionsTests
{
    [TestMethod]
    [DataRow("123", 123.0)]
    [DataRow("234.5", 234.5)]
    [DataRow("23rd", 23.0)]
    [DataRow("345.2R123",345.2)]
    public void ValTest(String value, Double expected)
    {
        var result = value.Val();
        Assert.AreEqual(expected, result);
    }
}
