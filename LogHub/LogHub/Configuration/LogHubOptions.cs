namespace LogHub.Configuration;

/// <summary>
/// Опции LogHub из appsettings.json.
/// SourceType: "file" | "csv" | "memory".
/// </summary>
public sealed class LogHubOptions
{
    public string SourceType { get; init; } = "file";
    public string SourcePath { get; init; } = "data/mixed.log";
}