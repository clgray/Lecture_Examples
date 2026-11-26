using LogHub.Models;

namespace LogHub.Parsing;

public interface ILogParser
{
    string Name { get; }
    bool CanParse(string line);
    LogRecord Parse(string line);
}