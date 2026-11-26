namespace LogHub.Graphs;

/// <summary>
/// Граф зависимостей сервисов. Только структура, без обходов.
/// Обходы — отдельные классы (ServiceGraphDfs, ServiceGraphBfs),
/// чтобы коллекция НЕ была сама себе итератором (антипример).
/// </summary>
public sealed class ServiceGraph
{
    private readonly Dictionary<string, ServiceNode> _nodes = new();

    public ServiceNode GetOrAdd(string id)
    {
        if (!_nodes.TryGetValue(id, out var node))
        {
            node = new ServiceNode(id);
            _nodes[id] = node;
        }
        return node;
    }

    public ServiceNode this[string id] => _nodes[id];
    public IReadOnlyCollection<ServiceNode> Nodes => _nodes.Values;
}