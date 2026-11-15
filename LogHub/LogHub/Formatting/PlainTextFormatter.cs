using LogHub.Models;

namespace LogHub.Formatting;

public class PlainTextFormatter
{
    public string Format(LogRecord record)
    {
        return $"{record.IpAddress} [{record.Date:dd/MMM/yyyy:HH:mm:ss zzz}] " +
               $"\"{record.Request}\" {record.StatusCode} {record.ResponseSize} " +
               $"\"{record.UserAgent}\"";
    }
}