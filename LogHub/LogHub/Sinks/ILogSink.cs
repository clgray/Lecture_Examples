using LogHub.Models;

namespace LogHub.Sinks;

public interface ILogSink
{
    void Write(LogRecord record);
}