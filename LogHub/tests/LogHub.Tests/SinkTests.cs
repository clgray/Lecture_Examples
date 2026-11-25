using LogHub.Config;
using LogHub.Domain;
using LogHub.Processing;
using LogHub.Sinks;
using Xunit;

namespace LogHub.Tests;

public class SinkTests
{
    private static readonly DateTime Moment = new(2026, 9, 14, 9, 0, 0);

    [Fact]
    public void ConsoleSink_Error_IsRedAndColorIsRestored()
    {
        var console = new FakeConsole { ForegroundColor = ConsoleColor.Cyan };
        var sink = new ConsoleSink(console);

        sink.Write(new LogRecord(Moment, "Error", "сбой"), "строка");

        Assert.Equal(ConsoleColor.Red, console.ColorsAtWrite[0]);
        Assert.Equal(ConsoleColor.Cyan, console.ForegroundColor);
    }

    [Fact]
    public void ConsoleSink_Info_IsGray()
    {
        var console = new FakeConsole();
        var sink = new ConsoleSink(console);

        sink.Write(new LogRecord(Moment, "Info", "ок"), "строка");

        Assert.Equal(ConsoleColor.Gray, console.ColorsAtWrite[0]);
    }

    [Fact]
    public void FileSink_WritesLineAndFlushes()
    {
        var writer = new StringWriter();
        var sink = new FileSink(writer);

        sink.Write(new LogRecord(Moment, "Info", "ок"), "строка");

        Assert.Equal("строка" + Environment.NewLine, writer.ToString());
    }

    [Fact]
    public void DatabaseSink_WritesAndClosesOnDispose()
    {
        var client = new RecordingDbClient();
        var sink = new DatabaseSink(client);

        sink.Write(new LogRecord(Moment, "Info", "ок"), "строка");
        sink.Dispose();

        Assert.Equal(new[] { "execute:Info:строка", "close" }, client.Calls);
    }

    [Fact]
    public void Registry_UnknownSink_ThrowsWithSameMessageAsBefore()
    {
        var registry = new SinkRegistry(new ILogSinkFactory[] { new ConsoleSinkFactory() });
        var config = LogHubConfig.Parse(new[] { "sink = ftp" });

        var error = Assert.Throws<NotSupportedException>(() => registry.Create(config));

        Assert.Equal("Приёмник 'ftp' не поддерживается", error.Message);
    }

    [Fact]
    public void Registry_NewSinkFactory_IsUsedWithoutChangingExistingCode()
    {
        var registry = new SinkRegistry(new ILogSinkFactory[] { new ConsoleSinkFactory(), new TelegramSinkFactory() });
        var config = LogHubConfig.Parse(new[] { "sink = telegram" });

        using var sink = registry.Create(config);

        Assert.IsType<RecordingSink>(sink);
    }

    [Fact]
    public void Catalog_FindsBuiltInSinkFactories()
    {
        var names = PluginCatalog.Discover<ILogSinkFactory>().Select(f => f.Name).ToList();

        Assert.Contains("console", names);
        Assert.Contains("file", names);
        Assert.Contains("database", names);
    }
}
