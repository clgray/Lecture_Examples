using LogHub.Domain;

namespace LogHub.Sinks;

/// <summary>
/// Приёмник умеет одно: принять запись. Остальные методы «универсального»
/// интерфейса из исходного файла (транзакции, ротация, цвет) никто не вызывал.
/// </summary>
public interface ILogSink : IDisposable
{
    void Write(LogRecord record, string text);
}
