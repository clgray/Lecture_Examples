using LogHub.Config;
using LogHub.Domain;

namespace LogHub.Sinks;

public interface IDbClient
{
    void Open();

    void Execute(string sql, DateTime date, string severity, string text);

    void Close();
}

/// <summary>
/// Заглушка клиента БД, по образцу FakeDbConnection из учебного репозитория
/// (_AntiPatterns/GodObject/LogProcessingModule.cs). Написана заново, не скопирована.
/// </summary>
public sealed class FakeDbClient : IDbClient
{
    private readonly string _connectionString;
    private bool _open;

    public FakeDbClient(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void Open()
    {
        _open = true;
        Console.WriteLine($"[БД] соединение открыто: {_connectionString}");
    }

    public void Execute(string sql, DateTime date, string severity, string text)
    {
        if (!_open)
        {
            throw new InvalidOperationException("Соединение не открыто");
        }

        Console.WriteLine($"[БД] {sql} -> {date:O}, {severity}, {text}");
    }

    public void Close()
    {
        _open = false;
        Console.WriteLine("[БД] соединение закрыто");
    }
}

public sealed class DatabaseSink : ILogSink
{
    private const string InsertSql = "INSERT INTO logs(date, severity, message) VALUES(@d, @s, @m)";

    private readonly IDbClient _client;

    /// <summary>Клиент к этому моменту уже открыт; закрывает его Dispose.</summary>
    public DatabaseSink(IDbClient client)
    {
        _client = client;
    }

    public void Write(LogRecord record, string text)
    {
        _client.Execute(InsertSql, record.Date, record.Severity, text);
    }

    public void Dispose()
    {
        _client.Close();
    }
}

public sealed class DatabaseSinkFactory : ILogSinkFactory
{
    public string Name => "database";

    public ILogSink Create(LogHubConfig config)
    {
        var client = new FakeDbClient(config.Connection);
        client.Open();
        return new DatabaseSink(client);
    }
}
