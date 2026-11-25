using LogHub.Domain;
using LogHub.Formatting;
using Xunit;

namespace LogHub.Tests;

public class FormatterTests
{
    private static readonly LogRecord Sample =
        new(new DateTime(2026, 9, 14, 16, 42, 0), "Error", "сбой \"диска\"");

    [Fact]
    public void Plain_FormatsDateLevelAndMessage()
    {
        Assert.Equal("2026-09-14 16:42:00 [Error] сбой \"диска\"", new PlainFormatter().Format(Sample));
    }

    [Fact]
    public void Json_EscapesQuotesAndLowercasesLevel()
    {
        var expected = "{\"date\":\"2026-09-14T16:42:00.0000000\",\"level\":\"error\",\"message\":\"сбой \\\"диска\\\"\"}";

        Assert.Equal(expected, new JsonFormatter().Format(Sample));
    }

    [Fact]
    public void Csv_SeparatesFieldsWithSemicolon()
    {
        Assert.Equal("2026-09-14T16:42:00.0000000;Error;сбой \"диска\"", new CsvFormatter().Format(Sample));
    }

    [Fact]
    public void Registry_UnknownName_FallsBackToMessageOnly()
    {
        var registry = new FormatterRegistry(new ILogFormatter[] { new PlainFormatter() });

        Assert.Equal("сбой \"диска\"", registry.Get("xml").Format(Sample));
    }

    [Fact]
    public void Catalog_FindsAllFormattersInAssembly()
    {
        var names = Processing.PluginCatalog.Discover<ILogFormatter>().Select(f => f.Name).ToList();

        Assert.Contains("plain", names);
        Assert.Contains("json", names);
        Assert.Contains("csv", names);
    }
}
