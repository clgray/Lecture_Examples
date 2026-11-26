using LogHub.Models;

namespace LogHub.Parsing;

/// <summary>
/// Адаптер над ApacheLogParser (ЛР4): приводит его контракт
/// (LogRecord? + return null) к контракту ILogParser
/// (bool CanParse + LogRecord Parse + throw FormatException).
/// </summary>
public sealed class ApacheLogParserAdapter : ILogParser
{
    private readonly ApacheLogParser _inner;

    public ApacheLogParserAdapter(ApacheLogParser inner)
        => _inner = inner ?? throw new ArgumentNullException(nameof(inner));

    public string Name => "apache";

    public bool CanParse(string line)
    {
        if (string.IsNullOrWhiteSpace(line)) return false;
        return line.Contains("\"GET ") || line.Contains("\"POST ")
            || line.Contains("\"HEAD ") || line.Contains("\"PUT ")
            || line.Contains("\"DELETE ") || line.Contains("\"PATCH ")
            || line.Contains("\"OPTIONS ");
    }

    public LogRecord Parse(string line)
    {
        var record = _inner.Parse(line)
            ?? throw new FormatException($"Не Apache Common Log: {line}");

        return new LogRecord
        {
            IpAddress      = record.IpAddress,
            UserIdentifier = record.UserIdentifier,
            UserName       = record.UserName,
            Date           = record.Date,
            Request        = record.Request,
            StatusCode     = record.StatusCode,
            ResponseSize   = record.ResponseSize,
            UserAgent      = record.UserAgent,

            Level        = StatusToLevel(record.StatusCode),
            Message      = record.Request ?? string.Empty,
            SourceFormat = Name
        };
    }

    private static string StatusToLevel(int status) => status switch
    {
        >= 500 => "ERROR",
        >= 400 => "WARN",
        >= 300 => "INFO",
        _      => "DEBUG"
    };
}