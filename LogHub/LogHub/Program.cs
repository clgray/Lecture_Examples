using System.Text.Json;
using LogHub;
using LogHub.Configuration;
using LogHub.Filtering;
using LogHub.Formatting;
using LogHub.Importing;
using LogHub.Parsing;
using LogHub.Reading;
using LogHub.Sinks;

// ── Режим демонстрации графа (часть В) ──
// Запуск: dotnet run --project LogHub -- graph
// Не читает конфиг, не создаёт источник — сразу демо и выход.
if (args.Length > 0 && args[0] == "graph")
{
    LogHub.Graphs.ServiceGraphDemo.Run();
    return;
}

// ── 1. Конфигурация ──
var optionsPath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
var options = File.Exists(optionsPath)
    ? JsonSerializer.Deserialize<JsonElement>(File.ReadAllText(optionsPath))
          .GetProperty("LogHub")
          .Deserialize<LogHubOptions>() ?? new LogHubOptions()
    : new LogHubOptions();

// ── 2. Источник — из конфигурации (без if, без пересборки) ──
var source = LogSourceFactory.Create(options);

// ── 3. Импортёр с автоопределением формата (вариант 20) ──
ILogImporter importer = new MixedFormatImporter(new ILogParser[]
{
    new JsonLinesLogParser(),
    new ApacheLogParserAdapter(new ApacheLogParser()),
    new PlainTextLogParser()
});

// ── 4. Фильтр и приёмники ──
var filter = new SeverityFilter(minimumStatusCode: 0);
var formatter = new PlainTextFormatter();
var sinks = new ILogSink[]
{
    new ConsoleLogSink(formatter),
    new FileLogSink("out.log", formatter)
};

// ── 5. Процессор ──
var processor = new LogProcessor(importer, filter, sinks);

// ── 6. Запуск ──
var (written, skipped) = processor.Process(source);
Console.WriteLine($"Источник: {source.Name} ({options.SourceType})");
Console.WriteLine($"Записано: {written}, пропущено: {skipped}");