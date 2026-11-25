namespace LogHub.Domain;

public sealed record LogRecord(DateTime Date, string Severity, string Message);
