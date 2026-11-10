namespace LogHub.Configuration;

public sealed class LazyConfigService : IConfigService
{
    private static readonly Lazy<LazyConfigService> _instance =
        new(() => new LazyConfigService());

    private readonly Dictionary<string, string> _settings = new();

    private LazyConfigService()
    {
    }

    public static LazyConfigService Instance => _instance.Value;

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