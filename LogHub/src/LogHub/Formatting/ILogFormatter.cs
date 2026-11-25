using LogHub.Domain;

namespace LogHub.Formatting;

public interface ILogFormatter
{
    /// <summary>Имя, под которым формат указывается в конфигурации.</summary>
    string Name { get; }

    string Format(LogRecord record);
}
