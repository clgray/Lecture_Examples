using LogHub.Domain;

namespace LogHub.Formatting;

/// <summary>Формат по умолчанию: только текст сообщения (как ветка default в исходном switch).</summary>
public sealed class MessageOnlyFormatter : ILogFormatter
{
    public string Name => "message";

    public string Format(LogRecord record)
    {
        return record.Message;
    }
}
