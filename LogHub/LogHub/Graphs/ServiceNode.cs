namespace LogHub.Graphs;

/// <summary>
/// Узел графа зависимостей сервисов. Узел — сервис, ребро — «зависит от».
/// </summary>
public sealed class ServiceNode
{
    public string Id { get; }
    public List<ServiceNode> Dependencies { get; } = new();

    public ServiceNode(string id)
        => Id = id ?? throw new ArgumentNullException(nameof(id));

    public ServiceNode AddDependency(ServiceNode dependency)
    {
        if (dependency is null) throw new ArgumentNullException(nameof(dependency));
        Dependencies.Add(dependency);
        return this;
    }

    public override string ToString() => Id;
}