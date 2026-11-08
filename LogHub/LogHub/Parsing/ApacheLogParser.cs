using System.Globalization;
using System.Text.RegularExpressions;
using LogHub.Models;

namespace LogHub.Parsing;

public class ApacheLogParser
{
    private static readonly Regex LogPattern = new(
        "^(\\S+) (\\S+) (\\S+) \\[([^\\]]+)\\] \"([^\"]*)\" (\\d{3}) (\\S+) \"([^\"]*)\" \"([^\"]*)\"$",
        RegexOptions.Compiled);

    public LogRecord? Parse(string line)
    {
        var match = LogPattern.Match(line);

        if (!match.Success)
            return null;

        if (!DateTime.TryParseExact(
                match.Groups[4].Value,
                "dd/MMM/yyyy:HH:mm:ss zzz",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var date))
        {
            return null;
        }

        if (!int.TryParse(match.Groups[6].Value, out var statusCode))
            return null;

        var responseSize = match.Groups[7].Value == "-"
            ? 0
            : long.TryParse(match.Groups[7].Value, out var size) ? size : -1;

        if (responseSize < 0)
            return null;

        return new LogRecord
        {
            IpAddress = match.Groups[1].Value,
            UserIdentifier = match.Groups[2].Value,
            UserName = match.Groups[3].Value,
            Date = date,
            Request = match.Groups[5].Value,
            StatusCode = statusCode,
            ResponseSize = responseSize,
            UserAgent = match.Groups[9].Value
        };
    }
}