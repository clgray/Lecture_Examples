namespace LogHub.Config;

/// <summary>
/// Настройки в виде пар «ключ = значение». Разбор строк не трогает диск:
/// файл читает точка входа и передаёт сюда готовые строки.
/// </summary>
public sealed class LogHubConfig
{
    private readonly IReadOnlyDictionary<string, string> _values;

    public LogHubConfig(IReadOnlyDictionary<string, string> values)
    {
        _values = values;
    }

    public string Sink => Get("sink", "console");

    public string MinSeverity => Get("minSeverity", "Info");

    public string Format => Get("format", "plain");

    public string Connection => Get("connection", "local");

    /// <summary>Разделитель полей во входных логах (по умолчанию «|»).</summary>
    public char Delimiter
    {
        get
        {
            var value = Get("delimiter", "|");
            return value.Length > 0 ? value[0] : '|';
        }
    }

    public string Get(string key, string defaultValue)
    {
        return _values.TryGetValue(key, out var value) ? value : defaultValue;
    }

    public static LogHubConfig Parse(IEnumerable<string> lines)
    {
        var values = new Dictionary<string, string>();

        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line) || line.StartsWith('#'))
            {
                continue;
            }

            var parts = line.Split('=', 2);
            if (parts.Length == 2)
            {
                values[parts[0].Trim()] = parts[1].Trim();
            }
        }

        return new LogHubConfig(values);
    }
}
