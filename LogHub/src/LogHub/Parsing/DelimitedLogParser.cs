using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using LogHub.Domain;

namespace LogHub.Parsing;

/// <summary>Строка вида «дата{разделитель}уровень{разделитель}сообщение».</summary>
public sealed class DelimitedLogParser : ILogParser
{
    private readonly char _delimiter;

    public DelimitedLogParser(char delimiter)
    {
        _delimiter = delimiter;
    }

    public bool TryParse(string line, [NotNullWhen(true)] out LogRecord? record)
    {
        record = null;

        var parts = line.Split(_delimiter, 3);
        if (parts.Length != 3)
        {
            return false;
        }

        if (!DateTime.TryParse(parts[0], CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
        {
            return false;
        }

        record = new LogRecord(date, parts[1].Trim(), parts[2].Trim());
        return true;
    }
}
