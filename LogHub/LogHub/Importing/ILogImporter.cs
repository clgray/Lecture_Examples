using LogHub.Models;
using LogHub.Reading;

namespace LogHub.Importing;

public interface ILogImporter
{
    IEnumerable<LogRecord> Import(ILogSource source);
}