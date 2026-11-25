using LogHub.Domain;
using Xunit;

namespace LogHub.Tests;

public class SeverityScaleTests
{
    [Theory]
    [InlineData("Debug", 0)]
    [InlineData("Info", 1)]
    [InlineData("Warning", 2)]
    [InlineData("Error", 3)]
    [InlineData("Critical", 4)]
    public void Rank_KnownLevels_AreOrdered(string level, int expected)
    {
        Assert.Equal(expected, SeverityScale.Rank(level));
    }

    [Fact]
    public void Rank_UnknownLevel_IsTreatedAsInfo()
    {
        Assert.Equal(SeverityScale.Rank("Info"), SeverityScale.Rank("Trace"));
    }

    [Theory]
    [InlineData("Error", true)]
    [InlineData("Critical", true)]
    [InlineData("Warning", false)]
    public void IsAlarming_OnlyErrorAndCritical(string level, bool expected)
    {
        Assert.Equal(expected, SeverityScale.IsAlarming(level));
    }
}
