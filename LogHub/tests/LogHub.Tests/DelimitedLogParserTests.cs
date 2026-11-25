using LogHub.Parsing;
using Xunit;

namespace LogHub.Tests;

public class DelimitedLogParserTests
{
    [Fact]
    public void TryParse_ValidLine_ReturnsRecord()
    {
        var parser = new DelimitedLogParser('|');

        var ok = parser.TryParse("2026-09-14 16:42:00|Error| диск полон ", out var record);

        Assert.True(ok);
        Assert.NotNull(record);
        Assert.Equal(new DateTime(2026, 9, 14, 16, 42, 0), record.Date);
        Assert.Equal("Error", record.Severity);
        Assert.Equal("диск полон", record.Message);
    }

    [Fact]
    public void TryParse_MessageContainingDelimiter_KeepsItInMessage()
    {
        var parser = new DelimitedLogParser('|');

        parser.TryParse("2026-09-14 16:42:00|Info|a|b|c", out var record);

        Assert.NotNull(record);
        Assert.Equal("a|b|c", record.Message);
    }

    [Fact]
    public void TryParse_TooFewFields_ReturnsFalse()
    {
        var parser = new DelimitedLogParser('|');

        Assert.False(parser.TryParse("2026-09-14|Info", out _));
    }

    [Fact]
    public void TryParse_BadDate_ReturnsFalse()
    {
        var parser = new DelimitedLogParser('|');

        Assert.False(parser.TryParse("не-дата|Info|текст", out _));
    }

    [Fact]
    public void TryParse_CustomDelimiter_Works()
    {
        var parser = new DelimitedLogParser(';');

        var ok = parser.TryParse("2026-09-14 16:42:00;Info;текст", out var record);

        Assert.True(ok);
        Assert.NotNull(record);
        Assert.Equal("текст", record.Message);
    }
}
