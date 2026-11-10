using LogHub.Formatting;
using LogHub.Models;

namespace LogHub.Sinks;

public class ConsoleLogSink : ILogSink
{
    private readonly PlainTextFormatter _formatter;

    public ConsoleLogSink(PlainTextFormatter formatter)
    {
        _formatter = formatter;
    }

    public void Write(LogRecord record)
    {
        Console.WriteLine(_formatter.Format(record));
    }
}