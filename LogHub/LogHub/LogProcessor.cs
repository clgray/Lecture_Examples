using LogHub.Filtering;
using LogHub.Formatting;
using LogHub.Importing;
using LogHub.Parsing;
using LogHub.Reading;
using LogHub.Sinks;

namespace LogHub;

public class LogProcessor
{
    private readonly ILogImporter _importer;
    private readonly SeverityFilter _filter;
    private readonly IEnumerable<ILogSink> _sinks;

    // ── Новый конструктор (ЛР6): абстракции вместо конкретики ──
    public LogProcessor(
        ILogImporter importer,
        SeverityFilter filter,
        IEnumerable<ILogSink> sinks)
    {
        _importer = importer ?? throw new ArgumentNullException(nameof(importer));
        _filter   = filter   ?? throw new ArgumentNullException(nameof(filter));
        _sinks    = sinks    ?? throw new ArgumentNullException(nameof(sinks));
    }

    // ── Старый конструктор (ЛР4): обратная совместимость ──
    public LogProcessor(
        LogFileReader reader,
        ApacheLogParser parser,
        SeverityFilter filter,
        PlainTextFormatter formatter,
        IEnumerable<ILogSink> sinks)
        : this(
            new SingleFormatImporter(new ApacheLogParserAdapter(parser)),
            filter,
            sinks)
    {
        _ = reader;
        _ = formatter;
    }

    public (int Written, int Skipped) Process(string filePath)
        => Process(new FileLogSource(filePath));

    public (int Written, int Skipped) Process(ILogSource source)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));

        int written = 0;
        int skipped = 0;

        foreach (var record in _importer.Import(source))
        {
            if (record.Level == "BAD_FORMAT") { skipped++; continue; }
            if (!_filter.IsAllowed(record))   { skipped++; continue; }

            foreach (var sink in _sinks)
                sink.Write(record);

            written++;
        }
        return (written, skipped);
    }
}