using LogHub.Config;
using LogHub.Domain;
using LogHub.Sinks;

namespace LogHub.Tests;

/// <summary>Приёмник, который просто запоминает записи. Ни диска, ни консоли.</summary>
internal sealed class RecordingSink : ILogSink
{
    public List<(LogRecord Record, string Text)> Items { get; } = new();

    public bool Disposed { get; private set; }

    public void Write(LogRecord record, string text)
    {
        Items.Add((record, text));
    }

    public void Dispose()
    {
        Disposed = true;
    }
}

internal sealed class FakeConsole : IConsoleOutput
{
    public ConsoleColor ForegroundColor { get; set; } = ConsoleColor.White;

    public List<string> Lines { get; } = new();

    public List<ConsoleColor> ColorsAtWrite { get; } = new();

    public void WriteLine(string text)
    {
        ColorsAtWrite.Add(ForegroundColor);
        Lines.Add(text);
    }
}

internal sealed class RecordingDbClient : IDbClient
{
    public List<string> Calls { get; } = new();

    public void Open() => Calls.Add("open");

    public void Execute(string sql, DateTime date, string severity, string text) =>
        Calls.Add($"execute:{severity}:{text}");

    public void Close() => Calls.Add("close");
}

/// <summary>Новый приёмник, добавленный «снаружи»: ни один файл LogHub для этого не менялся.</summary>
internal sealed class TelegramSinkFactory : ILogSinkFactory
{
    public string Name => "telegram";

    public ILogSink Create(LogHubConfig config) => new RecordingSink();
}
