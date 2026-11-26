using LogHub.Importing;
using LogHub.Parsing;
using LogHub.Reading;

namespace LogHub.Tests.Importing;

public class StrategySubstitutionTests
{
    [Fact]
    public void SwappingParser_ChangesResult()
    {
        // Одна и та же строка, два разных набора парсеров
        var line = "[2024-05-01 10:00:01] INFO hello";
        var source = new InMemoryLogSource(new[] { line });

        // Набор 1: только PlainTextLogParser
        var plainOnly = new MixedFormatImporter(new ILogParser[]
        {
            new PlainTextLogParser()
        });
        var record1 = plainOnly.Import(source).Single();
        Assert.Equal("plain", record1.SourceFormat);
        Assert.Equal("INFO", record1.Level);

        // Набор 2: только JsonLinesLogParser (та же строка — не JSON)
        var jsonOnly = new MixedFormatImporter(new ILogParser[]
        {
            new JsonLinesLogParser()
        });
        var record2 = jsonOnly.Import(source).Single();
        Assert.Equal("BAD_FORMAT", record2.Level);

        // Результат отличается — стратегия действительно влияет
        Assert.NotEqual(record1.SourceFormat, record2.SourceFormat);
    }
}