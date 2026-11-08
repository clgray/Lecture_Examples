using LogHub;
using LogHub.Filtering;
using LogHub.Formatting;
using LogHub.Parsing;
using LogHub.Reading;
using LogHub.Sinks;

var reader = new LogFileReader();
var parser = new ApacheLogParser();
var filter = new SeverityFilter(200);
var formatter = new PlainTextFormatter();

var memorySink = new InMemoryLogSink();
var fileSink = new FileLogSink("logs.txt", formatter);

var processor = new LogProcessor(
    reader,
    parser,
    filter,
    formatter,
    new ILogSink[] { memorySink, fileSink });

var inputFile = args.Length > 0 ? args[0] : "access.log";

var result = processor.Process(inputFile);

Console.WriteLine($"Записано: {result.Written}");
Console.WriteLine($"Пропущено: {result.Skipped}");
Console.WriteLine($"В памяти сохранено: {memorySink.Records.Count}");