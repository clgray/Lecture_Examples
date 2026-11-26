namespace LogHub.Reading;

/// <summary>
/// Источник строк лога. Стратегия: файл, CSV, in-memory.
/// </summary>
public interface ILogSource
{
    string Name { get; }
    IEnumerable<string> ReadLines();
}