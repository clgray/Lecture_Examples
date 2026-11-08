using LogHub.Models;
using LogHub.Parsing;
using LogHub.Sinks;
using LogHub.Filtering;

namespace LogHub.Tests;

public class UnitTest1
{
    private readonly ApacheLogParser _parser = new();

    // 1. Проверка разбора корректной записи
    [Fact]
    public void Parse_ValidLog_ReturnsRecord()
    {
        var line = "192.168.1.10 - - [08/Nov/2026:12:00:00 +0300] \"GET /index.html HTTP/1.1\" 200 1024 \"-\" \"Mozilla/5.0 Chrome/120.0\"";

        var result = _parser.Parse(line);

        Assert.NotNull(result);
        Assert.Equal("192.168.1.10", result.IpAddress);
        Assert.Equal(200, result.StatusCode);
    }

    // 2. Проверка извлечения User-Agent
    [Fact]
    public void Parse_ExtractsUserAgent()
    {
        var line = "192.168.1.10 - - [08/Nov/2026:12:00:00 +0300] \"GET / HTTP/1.1\" 200 100 \"-\" \"Mozilla/5.0 Chrome/120.0\"";

        var result = _parser.Parse(line);

        Assert.NotNull(result);
        Assert.Equal("Mozilla/5.0 Chrome/120.0", result.UserAgent);
    }

    // 3. Проверка некорректной строки
    [Fact]
    public void Parse_InvalidLine_ReturnsNull()
    {
        var result = _parser.Parse("invalid log line");

        Assert.Null(result);
    }

    // 4. Проверка отсутствующего размера ответа
    [Fact]
    public void Parse_MissingResponseSize_ReturnsZero()
    {
        var line = "192.168.1.10 - - [08/Nov/2026:12:00:00 +0300] \"GET / HTTP/1.1\" 200 - \"-\" \"Mozilla/5.0\"";

        var result = _parser.Parse(line);

        Assert.NotNull(result);
        Assert.Equal(0, result.ResponseSize);
    }

    // 5. Проверка сохранения записи в памяти
    [Fact]
    public void InMemoryLogSink_Write_SavesRecord()
    {
        var sink = new InMemoryLogSink();

        var record = new LogRecord
        {
            IpAddress = "192.168.1.10",
            Request = "GET /index.html HTTP/1.1",
            StatusCode = 200,
            UserAgent = "Mozilla/5.0"
        };

        sink.Write(record);

        Assert.Single(sink.Records);
        Assert.Same(record, sink.Records[0]);
    }

    // 6. Проверка фильтрации по HTTP-статусу
    [Fact]
    public void SeverityFilter_StatusBelowMinimum_ReturnsFalse()
    {
        var filter = new SeverityFilter(400);

        var record = new LogRecord
        {
            StatusCode = 200
        };

        var result = filter.IsAllowed(record);

        Assert.False(result);
    }
}