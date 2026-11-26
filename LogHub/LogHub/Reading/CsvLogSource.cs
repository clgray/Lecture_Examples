using System.Text;

namespace LogHub.Reading;

/// <summary>
/// Источник 2: CSV с заголовком timestamp,level,message.
/// Собирает строку обратно в формат mixed-потока:
///   - message начинается с '{' → отдаёт как JSON-строку;
///   - иначе → plain-text "[timestamp] LEVEL message".
/// </summary>
public sealed class CsvLogSource : ILogSource
{
    private readonly string _path;

    public CsvLogSource(string path)
        => _path = path ?? throw new ArgumentNullException(nameof(path));

    public string Name => "csv";

    public IEnumerable<string> ReadLines()
    {
        using var reader = new StreamReader(_path, detectEncodingFromByteOrderMarks: true);
        var header = reader.ReadLine();
        if (header is null) yield break;

        var columns = header.Split(',');
        int tsIdx  = Array.IndexOf(columns, "timestamp");
        int lvlIdx = Array.IndexOf(columns, "level");
        int msgIdx = Array.IndexOf(columns, "message");
        if (tsIdx < 0 || lvlIdx < 0 || msgIdx < 0)
            throw new FormatException("CSV: нужны колонки timestamp, level, message");

        string? line;
        while ((line = reader.ReadLine()) is not null)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            var parts = SplitCsvLine(line);
            var ts  = parts[tsIdx];
            var lvl = parts[lvlIdx];
            var msg = parts[msgIdx];

            yield return msg.TrimStart().StartsWith('{')
                ? msg
                : $"[{ts}] {lvl} {msg}";
        }
    }

    private static string[] SplitCsvLine(string line)
    {
        var result = new List<string>();
        var sb = new StringBuilder();
        bool inQuotes = false;

        for (int i = 0; i < line.Length; i++)
        {
            char c = line[i];
            if (c == '"')
            {
                if (inQuotes && i + 1 < line.Length && line[i + 1] == '"')
                { sb.Append('"'); i++; }
                else inQuotes = !inQuotes;
            }
            else if (c == ',' && !inQuotes)
            { result.Add(sb.ToString()); sb.Clear(); }
            else sb.Append(c);
        }
        result.Add(sb.ToString());
        return result.ToArray();
    }
}