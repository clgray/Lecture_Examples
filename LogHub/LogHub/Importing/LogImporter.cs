using LogHub.Models;
using LogHub.Reading;

namespace LogHub.Importing;

/// <summary>
/// Базовый импортёр. Шаблонный метод Import задаёт скелет:
///   открыть → прочитать → (валидация) → разобрать → закрыть → освободить.
///
/// - шаги «прочитать» и «разобрать» — абстрактные;
/// - шаг «валидация» — хук с реализацией по умолчанию;
/// - сам шаблонный метод — невиртуальный (наследники не могут его переопределить).
/// </summary>
public abstract class LogImporter : ILogImporter
{
    public IEnumerable<LogRecord> Import(ILogSource source)
    {
        ArgumentNullException.ThrowIfNull(source);

        // Шаг 1: открыть
        OpenSource(source);
        try
        {
            // Шаг 2: прочитать (абстрактный)
            foreach (var line in ReadLines(source))
            {
                // Шаг 3: валидация (хук с реализацией по умолчанию)
                if (!Validate(line))
                    continue;

                LogRecord record;
                try
                {
                    // Шаг 4: разобрать (абстрактный)
                    record = ParseLine(line);
                }
                catch (FormatException)
                {
                    record = BadFormat(line);
                }
                yield return record;
            }
        }
        finally
        {
            // Шаги 5 и 6: закрыть и освободить
            CloseSource(source);
            ReleaseSource(source);
        }
    }

    // ─── Абстрактные шаги ───────────────────────────────────────────

    protected abstract IEnumerable<string> ReadLines(ILogSource source);
    protected abstract LogRecord ParseLine(string line);

    // ─── Хуки с реализацией по умолчанию ────────────────────────────

    /// <summary>Хук «валидация». По умолчанию отбрасывает пустые строки.</summary>
    protected virtual bool Validate(string line)
        => !string.IsNullOrWhiteSpace(line);

    protected virtual void OpenSource(ILogSource source) { }
    protected virtual void CloseSource(ILogSource source) { }
    protected virtual void ReleaseSource(ILogSource source) { }

    // ─── Вспомогательное ────────────────────────────────────────────

    protected static LogRecord BadFormat(string line) => new()
    {
        Date = DateTime.UtcNow,
        Level = "BAD_FORMAT",
        Message = line,
        SourceFormat = "unknown"
    };
}