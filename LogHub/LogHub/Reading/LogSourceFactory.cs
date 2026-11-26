using LogHub.Configuration;

namespace LogHub.Reading;

/// <summary>
/// Единственное место, где выбирается конкретный ILogSource.
/// Композиционный корень для источников: добавление 4-го источника
/// правит только этот файл.
/// </summary>
public static class LogSourceFactory
{
    public static ILogSource Create(LogHubOptions options) => options.SourceType switch
    {
        "file"   => new FileLogSource(options.SourcePath),
        "csv"    => new CsvLogSource(options.SourcePath),
        "memory" => new InMemoryLogSource(new[]
        {
            "[2024-05-01 10:00:01] INFO from-memory",
            "{\"timestamp\":\"2024-05-01T10:00:02Z\",\"level\":\"WARN\",\"message\":\"also-memory\"}"
        }),
        _ => throw new ArgumentException($"Неизвестный SourceType: {options.SourceType}")
    };
}