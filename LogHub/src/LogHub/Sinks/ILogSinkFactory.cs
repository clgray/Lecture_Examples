using LogHub.Config;

namespace LogHub.Sinks;

public interface ILogSinkFactory
{
    /// <summary>Имя, под которым приёмник указывается в конфигурации.</summary>
    string Name { get; }

    ILogSink Create(LogHubConfig config);
}
