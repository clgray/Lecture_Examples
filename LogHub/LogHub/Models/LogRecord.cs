namespace LogHub.Models;

public class LogRecord
{
    public string IpAddress { get; set; } = string.Empty;

    public string UserIdentifier { get; set; } = string.Empty;

    public string UserName { get; set; } = string.Empty;

    public DateTime Date { get; set; }

    public string Request { get; set; } = string.Empty;

    public int StatusCode { get; set; }

    public long ResponseSize { get; set; }

    public string UserAgent { get; set; } = string.Empty;
}