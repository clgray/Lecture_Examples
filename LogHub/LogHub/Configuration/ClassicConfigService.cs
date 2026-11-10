namespace LogHub.Configuration;

public sealed class ClassicConfigService : IConfigService
{
    private static readonly ClassicConfigService _instance = new();

    private readonly Dictionary<string, string> _settings = new();

    private ClassicConfigService()
    {
    }

    public static ClassicConfigService Instance => _instance;

    public string? GetValue(string key)
    {
        return _settings.TryGetValue(key, out var value)
            ? value
            : null;
    }

    public void SetValue(string key, string value)
    {
        _settings[key] = value;
    }
}