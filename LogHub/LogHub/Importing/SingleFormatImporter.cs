using LogHub.Models;
using LogHub.Parsing;
using LogHub.Reading;

namespace LogHub.Importing;

/// <summary>
/// Импортёр с одним фиксированным парсером (обратная совместимость с ЛР4).
/// ПЕРЕОПРЕДЕЛЯЕТ хук Validate: строка без [ ] не может быть Apache-логом
/// и отбрасывается ещё ДО парсинга. Демонстрирует работу хука (п.4).
/// </summary>
public sealed class SingleFormatImporter : LogImporter
{
    private readonly ILogParser _parser;

    public SingleFormatImporter(ILogParser parser)
        => _parser = parser ?? throw new ArgumentNullException(nameof(parser));

    protected override IEnumerable<string> ReadLines(ILogSource source)
        => source.ReadLines();

    protected override LogRecord ParseLine(string line)
        => _parser.Parse(line);

    // ─── ХУК ПЕРЕОПРЕДЕЛЁН ──────────────────────────────────────────
    // Дополнительно к базовой проверке (непустая строка) требуем
    // квадратные скобки — без них Apache-строка невозможна.
    protected override bool Validate(string line)
        => base.Validate(line)
           && line.Contains('[')
           && line.Contains(']');
}