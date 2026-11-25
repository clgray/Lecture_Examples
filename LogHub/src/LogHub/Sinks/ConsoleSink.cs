using LogHub.Config;
using LogHub.Domain;

namespace LogHub.Sinks;

public interface IConsoleOutput
{
    ConsoleColor ForegroundColor { get; set; }

    void WriteLine(string text);
}

public sealed class SystemConsole : IConsoleOutput
{
    public ConsoleColor ForegroundColor
    {
        get => Console.ForegroundColor;
        set => Console.ForegroundColor = value;
    }

    public void WriteLine(string text)
    {
        Console.WriteLine(text);
    }
}

public sealed class ConsoleSink : ILogSink
{
    private readonly IConsoleOutput _console;

    public ConsoleSink(IConsoleOutput console)
    {
        _console = console;
    }

    public void Write(LogRecord record, string text)
    {
        var previous = _console.ForegroundColor;
        _console.ForegroundColor = SeverityScale.IsAlarming(record.Severity) ? ConsoleColor.Red : ConsoleColor.Gray;
        _console.WriteLine(text);
        _console.ForegroundColor = previous;
    }

    public void Dispose()
    {
    }
}

public sealed class ConsoleSinkFactory : ILogSinkFactory
{
    public string Name => "console";

    public ILogSink Create(LogHubConfig config)
    {
        return new ConsoleSink(new SystemConsole());
    }
}
