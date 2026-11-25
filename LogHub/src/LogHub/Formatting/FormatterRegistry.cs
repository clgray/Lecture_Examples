namespace LogHub.Formatting;

public sealed class FormatterRegistry
{
    private readonly Dictionary<string, ILogFormatter> _formatters;

    public FormatterRegistry(IEnumerable<ILogFormatter> formatters)
    {
        _formatters = formatters.ToDictionary(f => f.Name);
    }

    /// <summary>Неизвестное имя даёт формат «только сообщение», как раньше.</summary>
    public ILogFormatter Get(string name)
    {
        return _formatters.TryGetValue(name, out var formatter) ? formatter : new MessageOnlyFormatter();
    }
}
