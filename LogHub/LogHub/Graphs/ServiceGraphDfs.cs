namespace LogHub.Graphs;

/// <summary>
/// Обход графа в глубину (DFS) через yield return.
/// </summary>
public sealed class ServiceGraphDfs : IEnumerable<ServiceNode>
{
    private readonly ServiceNode _root;

    public ServiceGraphDfs(ServiceNode root)
        => _root = root ?? throw new ArgumentNullException(nameof(root));

    public IEnumerator<ServiceNode> GetEnumerator()
    {
        var visited = new HashSet<string>();
        var stack = new Stack<ServiceNode>();
        stack.Push(_root);

        while (stack.Count > 0)
        {
            var current = stack.Pop();
            if (!visited.Add(current.Id)) continue;

            yield return current;

            // Reverse, чтобы обход шёл в порядке добавления зависимостей
            foreach (var dep in current.Dependencies.AsEnumerable().Reverse())
                stack.Push(dep);
        }
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        => GetEnumerator();
}