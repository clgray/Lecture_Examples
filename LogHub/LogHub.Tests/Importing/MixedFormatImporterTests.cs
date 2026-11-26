using LogHub.Importing;
using LogHub.Parsing;
using LogHub.Reading;

namespace LogHub.Tests.Importing;

public class MixedFormatImportTests
{
    /// <summary>
    /// Импортёр варианта 20. Порядок парсеров — от специфичного к общему:
    /// JSON → Apache → Plain. JSON-строка, внутри которой есть "GET ",
    /// должна уйти в JSON, а не в Apache.
    /// </summary>
    private static MixedFormatImporter CreateImporter() =>
        new(new ILogParser[]
        {
            new JsonLinesLogParser(),
            new ApacheLogParserAdapter(new ApacheLogParser()),
            new PlainTextLogParser()
        });

    // ────────────────────────────────────────────────────────────────
    // 1. Смешанный поток: три формата + мусор → автоопределение
    // ────────────────────────────────────────────────────────────────
    [Fact]
    public void InMemory_AutoDetects_ThreeFormats_And_MarksBadLines()
    {
        var source = new InMemoryLogSource(new[]
        {
            "[2024-05-01 10:00:01] INFO hello",
            "{\"timestamp\":\"2024-05-01T10:00:02Z\",\"level\":\"WARN\",\"message\":\"careful\"}",
            "192.168.1.10 - - [01/May/2024:10:00:04 +0300] \"GET /a HTTP/1.1\" 200 1 \"-\" \"Mozilla/5.0\"",
            "not-a-log-line"
        });

        var list = CreateImporter().Import(source).ToList();

        Assert.Equal(4, list.Count);

        Assert.Equal("plain",  list[0].SourceFormat);
        Assert.Equal("INFO",   list[0].Level);
        Assert.Equal("hello",  list[0].Message);

        Assert.Equal("json",   list[1].SourceFormat);
        Assert.Equal("WARN",   list[1].Level);
        Assert.Equal("careful", list[1].Message);

        Assert.Equal("apache",        list[2].SourceFormat);
        Assert.Equal("192.168.1.10",  list[2].IpAddress);
        Assert.Equal(200,             list[2].StatusCode);
        Assert.Equal("Mozilla/5.0",   list[2].UserAgent);

        Assert.Equal("BAD_FORMAT", list[3].Level);
        Assert.Equal("unknown",    list[3].SourceFormat);
    }

    // ────────────────────────────────────────────────────────────────
    // 2. Пустые строки и строки из пробелов пропускаются хуком Validate
    // ────────────────────────────────────────────────────────────────
    [Fact]
    public void InMemory_EmptyLines_AreSkipped()
    {
        var source = new InMemoryLogSource(new[]
        {
            "",
            "   ",
            "\t",
            "[2024-05-01 10:00:01] INFO a"
        });

        var list = CreateImporter().Import(source).ToList();

        Assert.Single(list);
        Assert.Equal("plain", list[0].SourceFormat);
    }

    // ────────────────────────────────────────────────────────────────
    // 3. Приоритет: JSON-строка с "GET " внутри message → JSON, не Apache
    // ────────────────────────────────────────────────────────────────
    [Fact]
    public void Priority_JsonWithHttpGet_IsParsedAsJson()
    {
        var json =
            "{\"timestamp\":\"2024-05-01T10:00:00Z\",\"level\":\"INFO\",\"message\":\"GET /x HTTP/1.1\"}";

        var source = new InMemoryLogSource(new[] { json });

        var list = CreateImporter().Import(source).ToList();

        Assert.Single(list);
        Assert.Equal("json", list[0].SourceFormat);
        Assert.Equal("INFO", list[0].Level);
        Assert.Equal("GET /x HTTP/1.1", list[0].Message);
    }

    // ────────────────────────────────────────────────────────────────
    // 4. Битый JSON: CanParse == true, но Parse падает → BAD_FORMAT
    // ────────────────────────────────────────────────────────────────
    [Fact]
    public void BadJson_ThatPassesCanParse_IsMarkedAsBadFormat()
    {
        var source = new InMemoryLogSource(new[] { "{\"bad json\":}" });

        var list = CreateImporter().Import(source).ToList();

        Assert.Single(list);
        Assert.Equal("BAD_FORMAT", list[0].Level);
        Assert.Equal("unknown",    list[0].SourceFormat);
        Assert.Equal("{\"bad json\":}", list[0].Message);
    }

    // ────────────────────────────────────────────────────────────────
    // 5. Ни один парсер не подошёл → BAD_FORMAT
    // ────────────────────────────────────────────────────────────────
    [Fact]
    public void NoParserMatched_IsMarkedAsBadFormat()
    {
        var source = new InMemoryLogSource(new[]
        {
            "!!! garbage line !!!",
            "this is not a log line at all"
        });

        var list = CreateImporter().Import(source).ToList();

        Assert.Equal(2, list.Count);
        Assert.All(list, r => Assert.Equal("BAD_FORMAT", r.Level));
        Assert.All(list, r => Assert.Equal("unknown",    r.SourceFormat));
    }

    // ────────────────────────────────────────────────────────────────
    // 6. Plain-парсер: извлекает поля
    // ────────────────────────────────────────────────────────────────
    [Fact]
    public void PlainText_Parse_ExtractsFields()
    {
        var parser = new PlainTextLogParser();

        Assert.True(parser.CanParse("[2024-05-01 10:00:01] WARN disk full"));

        var r = parser.Parse("[2024-05-01 10:00:01] WARN disk full");
        Assert.Equal("WARN",      r.Level);
        Assert.Equal("disk full", r.Message);
        Assert.Equal("plain",     r.SourceFormat);
        Assert.Equal(new DateTime(2024, 5, 1, 10, 0, 1), r.Date);
    }

    // ────────────────────────────────────────────────────────────────
    // 7. JSON-парсер: извлекает поля, CanParse отсеивает не-JSON
    // ────────────────────────────────────────────────────────────────
    [Fact]
    public void Json_Parse_ExtractsFields()
    {
        var parser = new JsonLinesLogParser();

        Assert.True(parser.CanParse(
            "{\"timestamp\":\"2024-05-01T10:00:03Z\",\"level\":\"ERROR\",\"message\":\"db down\"}"));
        Assert.False(parser.CanParse("[2024-05-01 10:00:01] INFO not json"));

        var r = parser.Parse(
            "{\"timestamp\":\"2024-05-01T10:00:03Z\",\"level\":\"ERROR\",\"message\":\"db down\"}");
        Assert.Equal("ERROR",   r.Level);
        Assert.Equal("db down", r.Message);
        Assert.Equal("json",    r.SourceFormat);
    }

    // ────────────────────────────────────────────────────────────────
    // 8. Apache-адаптер: Combined + маппинг StatusCode → Level
    // ────────────────────────────────────────────────────────────────
    [Fact]
    public void Apache_Adapter_ParsesAndMapsStatusToLevel()
    {
        var adapter = new ApacheLogParserAdapter(new ApacheLogParser());

        var line200 =
            "192.168.1.10 - - [01/May/2024:10:00:04 +0300] \"GET /a HTTP/1.1\" 200 1 \"-\" \"Mozilla/5.0\"";
        Assert.True(adapter.CanParse(line200));

        var r200 = adapter.Parse(line200);
        Assert.Equal("apache",       r200.SourceFormat);
        Assert.Equal("192.168.1.10", r200.IpAddress);
        Assert.Equal(200,            r200.StatusCode);
        Assert.Equal("DEBUG",        r200.Level);

        var line500 =
            "10.0.0.7 - - [01/May/2024:10:00:13 +0300] \"PUT /api/update HTTP/1.1\" 500 0 \"-\" \"curl/8.0\"";
        var r500 = adapter.Parse(line500);
        Assert.Equal("ERROR", r500.Level);
    }

    // ────────────────────────────────────────────────────────────────
    // 9. Хук по умолчанию: MixedFormatImporter НЕ переопределяет Validate,
    //    строка без скобок доходит до парсеров → BAD_FORMAT.
    // ────────────────────────────────────────────────────────────────
    [Fact]
    public void DefaultValidate_AllowsLineWithoutBrackets_ToParsers()
    {
        var source = new InMemoryLogSource(new[] { "not a log at all" });
        var importer = new MixedFormatImporter(new ILogParser[]
        {
            new PlainTextLogParser(),
            new JsonLinesLogParser()
        });

        var list = importer.Import(source).ToList();

        Assert.Single(list);
        Assert.Equal("BAD_FORMAT", list[0].Level);
        Assert.Equal("not a log at all", list[0].Message);
    }

    // ────────────────────────────────────────────────────────────────
    // 10. Хук переопределён: SingleFormatImporter отбрасывает строки
    //     без [ ] ещё ДО парсинга — их нет в результате вообще.
    // ────────────────────────────────────────────────────────────────
    [Fact]
    public void OverriddenValidate_FiltersLinesWithoutBrackets_BeforeParsing()
    {
        var source = new InMemoryLogSource(new[]
        {
            "192.168.1.10 - - [01/May/2024:10:00:04 +0300] \"GET /a HTTP/1.1\" 200 1 \"-\" \"Mozilla/5.0\"",
            "no brackets here at all"
        });

        var importer = new SingleFormatImporter(
            new ApacheLogParserAdapter(new ApacheLogParser()));

        var list = importer.Import(source).ToList();

        // Вторая строка отброшена хуком Validate — её нет в результате.
        Assert.Single(list);
        Assert.Equal("apache", list[0].SourceFormat);
    }
}