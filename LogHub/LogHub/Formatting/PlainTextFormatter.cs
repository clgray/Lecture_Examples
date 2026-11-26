using System.Globalization;
using LogHub.Models;

namespace LogHub.Formatting;

/// <summary>
/// Единый форматтер для всех источников.
/// Apache (Combined) — в привычном виде:
///   IP [дата] "request" status size "ua"
/// Остальные (plain, json, bad_format) — в виде:
///   yyyy-MM-dd HH:mm:ss [LEVEL] source: message
/// </summary>
public class PlainTextFormatter
{
    public string Format(LogRecord record)
    {
        ArgumentNullException.ThrowIfNull(record);

        return record.SourceFormat == "apache"
            ? FormatApache(record)
            : FormatGeneric(record);
    }

    private static string FormatApache(LogRecord r)
    {
        var date = r.Date.ToString("dd/MMM/yyyy:HH:mm:ss zzz", CultureInfo.InvariantCulture);
        return $"{r.IpAddress} [{date}] \"{r.Request}\" {r.StatusCode} {r.ResponseSize} \"{r.UserAgent}\"";
    }

    private static string FormatGeneric(LogRecord r)
    {
        var date = r.Date.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
        return $"{date} [{r.Level}] {r.SourceFormat}: {r.Message}";
    }
}