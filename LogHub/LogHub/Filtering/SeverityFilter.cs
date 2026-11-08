using LogHub.Models;

namespace LogHub.Filtering;

public class SeverityFilter
{
    private readonly int _minimumStatusCode;

    public SeverityFilter(int minimumStatusCode)
    {
        _minimumStatusCode = minimumStatusCode;
    }

    public bool IsAllowed(LogRecord record)
    {
        return record.StatusCode >= _minimumStatusCode;
    }
}