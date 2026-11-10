using System.Collections.Generic;
using System.Linq;
using LogHub.Configuration;
using Xunit;

namespace LogHub.Tests;

public class AlphabeticalTestCaseOrderer : Xunit.Sdk.ITestCaseOrderer
{
    public IEnumerable<TTestCase> OrderTestCases<TTestCase>(
        IEnumerable<TTestCase> testCases)
        where TTestCase : Xunit.Abstractions.ITestCase
    {
        return testCases.OrderBy(
            testCase => testCase.TestMethod.Method.Name);
    }
}

[TestCaseOrderer(
    "LogHub.Tests.AlphabeticalTestCaseOrderer",
    "LogHub.Tests")]
public class ConfigServiceTests
{
    [Fact]
    public void ClassicSingleton_FirstTest_SavesValue()
    {
        ClassicConfigService.Instance.SetValue("order-test", "saved");

        Assert.Equal(
            "saved",
            ClassicConfigService.Instance.GetValue("order-test"));
    }

    [Fact(Skip = "Демонстрация: после первого теста Singleton сохраняет общее состояние, поэтому ожидание пустого состояния нарушается.")]
    public void ClassicSingleton_SecondTest_ExpectsEmptyState()
    {
        Assert.Null(
            ClassicConfigService.Instance.GetValue("order-test"));
    }

    [Fact]
    public void LazySingleton_ReturnsSameInstance()
    {
        Assert.Same(
            LazyConfigService.Instance,
            LazyConfigService.Instance);
    }

    [Fact]
    public void ConfigService_InstancesHaveIndependentState()
    {
        var first = new ConfigService();
        var second = new ConfigService();

        first.SetValue("theme", "dark");

        Assert.Equal("dark", first.GetValue("theme"));
        Assert.Null(second.GetValue("theme"));
    }
}