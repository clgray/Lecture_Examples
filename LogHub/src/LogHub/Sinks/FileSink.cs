using LogHub.Config;
using LogHub.Domain;

namespace LogHub.Sinks;

public sealed class FileSink : ILogSink
{
    private readonly TextWriter _writer;

    public FileSink(TextWriter writer)
    {
        _writer = writer;
    }

    public void Write(LogRecord record, string text)
    {
        _writer.WriteLine(text);
        // Сброс на каждую запись оставлен как в исходном коде: поведение не меняем.
        _writer.Flush();
    }

    public void Dispose()
    {
        _writer.Dispose();
    }
}

public sealed class FileSinkFactory : ILogSinkFactory
{
    public string Name => "file";

    public ILogSink Create(LogHubConfig config)
    {
        var path = $"logs-{DateTime.Now:yyyyMMdd}.txt";
        return new FileSink(new StreamWriter(path, append: true));
    }
}
