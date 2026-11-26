using System.Text.Json;
using LogHub.Models;

namespace LogHub.Parsing;

public sealed class JsonLinesLogParser : ILogParser
{
    public string Name => "json";

    public bool CanParse(string line)
    {
        var t = line?.TrimStart();
        return t is not null && t.StartsWith('{') && t.EndsWith('}');
    }

    public LogRecord Parse(string line)
    {
        try
        {
            using var doc = JsonDocument.Parse(line);
            var root = doc.RootElement;
            return new LogRecord
            {
                Date = root.TryGetProperty("timestamp", out var ts)
                    ? ts.GetDateTime() : DateTime.UtcNow,
                Level = root.TryGetProperty("level", out var lvl)
                    ? lvl.GetString() ?? "UNKNOWN" : "UNKNOWN",
                Message = root.TryGetProperty("message", out var msg)
                    ? msg.GetString() ?? string.Empty : string.Empty,
                SourceFormat = Name
            };
        }
        catch (JsonException ex)
        {
            throw new FormatException($"Не JSON: {line}", ex);
        }
    }
}