using System.Collections;

namespace LogHub.Graphs;

/// <summary>
/// BFS-итератор, реализованный ВРУЧНУЮ через MoveNext/Current/Reset/Dispose.
/// Для сравнения с yield return (пункт 8 задания).
/// </summary>
public sealed class ServiceGraphBfsManual : IEnumerable<ServiceNode>, IEnumerator<ServiceNode>
{
    private readonly ServiceNode _root;
    private readonly Queue<ServiceNode> _queue = new();
    private readonly HashSet<string> _visited = new();
    private ServiceNode? _current;

    public ServiceGraphBfsManual(ServiceNode root)
    {
        _root = root ?? throw new ArgumentNullException(nameof(root));
        Reset();   // ← инициализируем очередь корнем сразу
    }

    public IEnumerator<ServiceNode> GetEnumerator()
    {
        Reset();
        return this;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public bool MoveNext()
    {
        while (_queue.Count > 0)
        {
            var node = _queue.Dequeue();
            if (!_visited.Add(node.Id)) continue;

            _current = node;

            foreach (var dep in node.Dependencies)
                _queue.Enqueue(dep);

            return true;
        }
        return false;
    }

    public void Reset()
    {
        _queue.Clear();
        _visited.Clear();
        _current = null;
        _queue.Enqueue(_root);
    }

    public ServiceNode Current => _current!;
    object IEnumerator.Current => Current;

    public void Dispose() { /* нечего освобождать */ }
}