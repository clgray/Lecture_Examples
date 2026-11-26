using System.Globalization;
using System.Text.RegularExpressions;
using LogHub.Models;

namespace LogHub.Parsing;

public sealed partial class PlainTextLogParser : ILogParser
{
    public string Name => "plain";

    [GeneratedRegex(@"^\[(?<ts>[^\]]+)\]\s+(?<lvl>[A-Z]+)\s+(?<msg>.+)$")]
    private static partial Regex Pattern();

    public bool CanParse(string line)
        => !string.IsNullOrWhiteSpace(line) && Pattern().IsMatch(line);

    public LogRecord Parse(string line)
    {
        var m = Pattern().Match(line);
        if (!m.Success)
            throw new FormatException($"Не plain-text: {line}");

        return new LogRecord
        {
            Date = DateTime.Parse(m.Groups["ts"].Value, CultureInfo.InvariantCulture),
            Level = m.Groups["lvl"].Value,
            Message = m.Groups["msg"].Value,
            SourceFormat = Name
        };
    }
}