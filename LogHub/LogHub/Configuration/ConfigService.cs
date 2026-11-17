namespace LogHub.Configuration;

public class ConfigService : IConfigService
{
    private readonly Dictionary<string, string> _settings = new();

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