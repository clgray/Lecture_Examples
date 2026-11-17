using Microsoft.Extensions.DependencyInjection;
using LogHub;
using LogHub.Filtering;
using LogHub.Formatting;
using LogHub.Parsing;
using LogHub.Reading;
using LogHub.Sinks;

var profilePath = Path.Combine(AppContext.BaseDirectory, "profile.txt");

var profile = File.Exists(profilePath)
    ? File.ReadAllText(profilePath).Trim().ToLowerInvariant()
    : "console";

var formatter = new PlainTextFormatter();
var memorySink = new InMemoryLogSink();

ILogSink outputSink = profile switch
{
    "console" => new ConsoleLogSink(formatter),
    "file" => new FileLogSink("logs.txt", formatter),
    _ => throw new InvalidOperationException(
        $"Неизвестный профиль: {profile}")
};

var services = new ServiceCollection();

services.AddSingleton<LogFileReader>();
services.AddSingleton<ApacheLogParser>();
services.AddSingleton<SeverityFilter>(_ => new SeverityFilter(200));

services.AddSingleton(formatter);
services.AddSingleton(memorySink);

services.AddSingleton<ILogSink>(memorySink);
services.AddSingleton<ILogSink>(outputSink);

services.AddSingleton<LogProcessor>();

using var provider = services.BuildServiceProvider();

var processor = provider.GetRequiredService<LogProcessor>();

var inputFile = args.Length > 0 ? args[0] : "access.log";

var result = processor.Process(inputFile);

Console.WriteLine($"Записано: {result.Written}");
Console.WriteLine($"Пропущено: {result.Skipped}");
Console.WriteLine($"В памяти сохранено: {memorySink.Records.Count}");