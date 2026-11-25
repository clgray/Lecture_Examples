using LogHub.Formatting;
using LogHub.Parsing;
using LogHub.Processing;
using Xunit;

namespace LogHub.Tests;

public class LogProcessorTests
{
    private static LogProcessor Create(RecordingSink sink, string minSeverity) =>
        new(new DelimitedLogParser('|'), new PlainFormatter(), sink, minSeverity);

    [Fact]
    public void Process_WritesParsedRecordsAboveThreshold()
    {
        var sink = new RecordingSink();
        var lines = new[]
        {
            "2026-09-14 09:00:00|Info|старт",
            "2026-09-14 09:01:00|Debug|подробности",
            "мусор",
            "2026-09-14 09:02:00|Error|сбой"
        };

        var result = Create(sink, "Info").Process(lines);

        Assert.Equal(2, result.Written);
        Assert.Equal(2, result.Skipped);
        Assert.Equal("2026-09-14 09:02:00 [Error] сбой", sink.Items[1].Text);
    }

    [Fact]
    public void Process_HigherThreshold_SkipsInfo()
    {
        var sink = new RecordingSink();
        var lines = new[]
        {
            "2026-09-14 09:00:00|Info|старт",
            "2026-09-14 09:02:00|Warning|диск"
        };

        var result = Create(sink, "Warning").Process(lines);

        Assert.Equal(1, result.Written);
        Assert.Equal(1, result.Skipped);
    }

    [Fact]
    public void Process_EmptyInput_WritesNothing()
    {
        var sink = new RecordingSink();

        var result = Create(sink, "Info").Process(Array.Empty<string>());

        Assert.Equal(0, result.Written);
        Assert.Equal(0, result.Skipped);
        Assert.Empty(sink.Items);
    }
}
