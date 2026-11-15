using LogHub.Formatting;
using LogHub.Models;

namespace LogHub.Sinks;

public class FileLogSink : ILogSink
{
    private readonly string _filePath;
    private readonly PlainTextFormatter _formatter;

    public FileLogSink(string filePath, PlainTextFormatter formatter)
    {
        _filePath = filePath;
        _formatter = formatter;
    }

    public void Write(LogRecord record)
    {
        var text = _formatter.Format(record);

        File.AppendAllText(_filePath, text + Environment.NewLine);
    }
}