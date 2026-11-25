using LogHub.Domain;

namespace LogHub.Formatting;

public sealed class CsvFormatter : ILogFormatter
{
    public string Name => "csv";

    public string Format(LogRecord record)
    {
        return $"{record.Date:O};{record.Severity};{record.Message}";
    }
}
