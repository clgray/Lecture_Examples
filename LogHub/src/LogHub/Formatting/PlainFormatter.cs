using LogHub.Domain;

namespace LogHub.Formatting;

public sealed class PlainFormatter : ILogFormatter
{
    public string Name => "plain";

    public string Format(LogRecord record)
    {
        return $"{record.Date:yyyy-MM-dd HH:mm:ss} [{record.Severity}] {record.Message}";
    }
}
