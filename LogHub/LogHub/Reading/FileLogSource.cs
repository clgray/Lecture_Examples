namespace LogHub.Reading;

/// <summary>
/// Источник 1: текстовый файл. Использует существующий LogFileReader из ЛР4.
/// </summary>
public sealed class FileLogSource : ILogSource
{
    private readonly string? _path;
    private readonly LogFileReader _reader;

    public FileLogSource(string path)
    {
        _path = path ?? throw new ArgumentNullException(nameof(path));
        _reader = new LogFileReader();
    }

    // Обратная совместимость: можно передать свой reader
    public FileLogSource(LogFileReader reader)
    {
        _reader = reader ?? throw new ArgumentNullException(nameof(reader));
        _path = null;
    }

    public string Name => "file";

    public IEnumerable<string> ReadLines()
    {
        if (_path is null)
            throw new InvalidOperationException(
                "FileLogSource создан без пути. Передайте путь в конструктор.");
        return _reader.ReadLines(_path);
    }
}