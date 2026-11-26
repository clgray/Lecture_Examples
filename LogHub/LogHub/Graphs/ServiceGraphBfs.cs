namespace LogHub.Graphs;

/// <summary>
/// Обход графа в ширину (BFS) через yield return.
/// Возвращает IEnumerable&lt;ServiceNode&gt; — можно вкладывать foreach.
/// </summary>
public sealed class ServiceGraphBfs : IEnumerable<ServiceNode>
{
    private readonly ServiceNode _root;

    public ServiceGraphBfs(ServiceNode root)
        => _root = root ?? throw new ArgumentNullException(nameof(root));

    public IEnumerator<ServiceNode> GetEnumerator()
    {
        var visited = new HashSet<string>();
        var queue = new Queue<ServiceNode>();
        queue.Enqueue(_root);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            if (!visited.Add(current.Id)) continue;

            yield return current;

            foreach (var dep in current.Dependencies)
                queue.Enqueue(dep);
        }
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        => GetEnumerator();
}