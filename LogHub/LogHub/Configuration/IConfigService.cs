namespace LogHub.Configuration;

public interface IConfigService
{
    string? GetValue(string key);

    void SetValue(string key, string value);
}
