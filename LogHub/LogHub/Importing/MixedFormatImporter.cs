using LogHub.Models;
using LogHub.Parsing;
using LogHub.Reading;

namespace LogHub.Importing;

/// <summary>
/// Импортёр смешанного потока (вариант 20).
/// Хук Validate НЕ переопределяет — работает с базовой реализацией
/// из LogImporter (пустая строка → skip).
///
/// Автоопределение формата:
///   1) перебрать парсеры по порядку;
///   2) первый, у которого CanParse == true — выбрать;
///   3) вызвать у него Parse;
///   4) если никто не подошёл — FormatException → BAD_FORMAT.
/// </summary>
public sealed class MixedFormatImporter : LogImporter
{
    private readonly IReadOnlyList<ILogParser> _parsers;

    public MixedFormatImporter(IEnumerable<ILogParser> parsers)
    {
        if (parsers is null)
            throw new ArgumentNullException(nameof(parsers));

        _parsers = parsers.ToList();

        if (_parsers.Count == 0)
            throw new ArgumentException(
                "Нужен хотя бы один парсер для MixedFormatImporter",
                nameof(parsers));
    }

    // Шаг «прочитать» — абстрактный в базовом классе
    protected override IEnumerable<string> ReadLines(ILogSource source)
        => source.ReadLines();

    // Шаг «разобрать» — абстрактный в базовом классе
    protected override LogRecord ParseLine(string line)
    {
        foreach (var parser in _parsers)
        {
            if (!parser.CanParse(line))
                continue;

            return parser.Parse(line);
        }

        throw new FormatException(
            $"Ни один парсер не подошёл для строки: {line}");
    }
}