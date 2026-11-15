using LogHub.Filtering;
using LogHub.Formatting;
using LogHub.Parsing;
using LogHub.Reading;
using LogHub.Sinks;

namespace LogHub;

public class LogProcessor
{
    private readonly LogFileReader _reader;
    private readonly ApacheLogParser _parser;
    private readonly SeverityFilter _filter;
    private readonly PlainTextFormatter _formatter;
    private readonly IEnumerable<ILogSink> _sinks;

    public LogProcessor(
        LogFileReader reader,
        ApacheLogParser parser,
        SeverityFilter filter,
        PlainTextFormatter formatter,
        IEnumerable<ILogSink> sinks)
    {
        _reader = reader;
        _parser = parser;
        _filter = filter;
        _formatter = formatter;
        _sinks = sinks;
    }

    public (int Written, int Skipped) Process(string filePath)
    {
        int written = 0;
        int skipped = 0;

        foreach (var line in _reader.ReadLines(filePath))
        {
            var record = _parser.Parse(line);

            if (record == null)
            {
                skipped++;
                continue;
            }

            if (!_filter.IsAllowed(record))
            {
                skipped++;
                continue;
            }

            foreach (var sink in _sinks)
            {
                sink.Write(record);
            }

            written++;
        }

        return (written, skipped);
    }
}