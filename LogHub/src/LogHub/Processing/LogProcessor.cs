using LogHub.Domain;
using LogHub.Formatting;
using LogHub.Parsing;
using LogHub.Sinks;

namespace LogHub.Processing;

public sealed record ProcessingResult(int Written, int Skipped);

/// <summary>Разбор → фильтр по уровню → форматирование → запись. Диска и консоли не знает.</summary>
public sealed class LogProcessor
{
    private readonly ILogParser _parser;
    private readonly ILogFormatter _formatter;
    private readonly ILogSink _sink;
    private readonly string _minSeverity;

    public LogProcessor(ILogParser parser, ILogFormatter formatter, ILogSink sink, string minSeverity)
    {
        _parser = parser;
        _formatter = formatter;
        _sink = sink;
        _minSeverity = minSeverity;
    }

    public ProcessingResult Process(IEnumerable<string> lines)
    {
        var minRank = SeverityScale.Rank(_minSeverity);
        var written = 0;
        var skipped = 0;

        foreach (var line in lines)
        {
            if (!_parser.TryParse(line, out var record))
            {
                skipped++;
                continue;
            }

            if (SeverityScale.Rank(record.Severity) < minRank)
            {
                skipped++;
                continue;
            }

            _sink.Write(record, _formatter.Format(record));
            written++;
        }

        return new ProcessingResult(written, skipped);
    }
}
