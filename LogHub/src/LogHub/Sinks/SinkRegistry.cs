using LogHub.Config;

namespace LogHub.Sinks;

public sealed class SinkRegistry
{
    private readonly Dictionary<string, ILogSinkFactory> _factories;

    public SinkRegistry(IEnumerable<ILogSinkFactory> factories)
    {
        _factories = factories.ToDictionary(f => f.Name);
    }

    public ILogSink Create(LogHubConfig config)
    {
        if (!_factories.TryGetValue(config.Sink, out var factory))
        {
            throw new NotSupportedException($"Приёмник '{config.Sink}' не поддерживается");
        }

        return factory.Create(config);
    }
}
