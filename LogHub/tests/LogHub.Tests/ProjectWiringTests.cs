using Xunit;

namespace LogHub.Tests;

public class ProjectWiringTests
{
    [Fact]
    public void TestProject_IsWiredAndRuns()
    {
        Assert.Equal(4, 2 + 2);
    }
}
