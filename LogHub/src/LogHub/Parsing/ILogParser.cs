using System.Diagnostics.CodeAnalysis;
using LogHub.Domain;

namespace LogHub.Parsing;

public interface ILogParser
{
    /// <summary>Возвращает false, если строку разобрать нельзя. Null наружу не отдаётся.</summary>
    bool TryParse(string line, [NotNullWhen(true)] out LogRecord? record);
}
