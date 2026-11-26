namespace LogHub.Models;
public class LogRecord
{
    // Apache-поля (уже есть)
    public string? IpAddress { get; init; }
    public string? UserIdentifier { get; init; }
    public string? UserName { get; init; }
    public DateTime Date { get; init; }
    public string? Request { get; init; }
    public int StatusCode { get; init; }
    public long ResponseSize { get; init; }
    public string? UserAgent { get; init; }

    // новые поля для mixed-потока
    public string Level { get; init; } = "UNKNOWN";
    public string Message { get; init; } = string.Empty;
    public string SourceFormat { get; init; } = "unknown";
}