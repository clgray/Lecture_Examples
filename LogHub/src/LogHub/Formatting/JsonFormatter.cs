using LogHub.Domain;

namespace LogHub.Formatting;

public sealed class JsonFormatter : ILogFormatter
{
    public string Name => "json";

    public string Format(LogRecord record)
    {
        return "{\"date\":\"" + record.Date.ToString("O") + "\"," +
               "\"level\":\"" + record.Severity.ToLowerInvariant() + "\"," +
               "\"message\":\"" + record.Message.Replace("\"", "\\\"") + "\"}";
    }
}
