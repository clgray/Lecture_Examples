namespace LogHub.Reading;

/// <summary>
/// Источник 3 (обязателен): строки уже в памяти.
/// Нужен для тестов, не трогающих диск/сеть/БД (критерий DIP).
/// </summary>
public sealed class InMemoryLogSource : ILogSource
{
    private readonly IReadOnlyList<string> _lines;

    public InMemoryLogSource(IEnumerable<string> lines)
        => _lines = lines?.ToList() ?? throw new ArgumentNullException(nameof(lines));

    public string Name => "in-memory";

    public IEnumerable<string> ReadLines() => _lines;
}