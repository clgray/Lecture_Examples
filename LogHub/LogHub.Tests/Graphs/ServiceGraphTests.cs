using LogHub.Graphs;

namespace LogHub.Tests.Graphs;

public class ServiceGraphTests
{
    private static (ServiceGraph graph, ServiceNode root) BuildGraph()
    {
        var g = new ServiceGraph();
        var a = g.GetOrAdd("A");
        var b = g.GetOrAdd("B");
        var c = g.GetOrAdd("C");
        var d = g.GetOrAdd("D");
        var e = g.GetOrAdd("E");

        a.AddDependency(b).AddDependency(c);
        b.AddDependency(d);
        c.AddDependency(d);
        d.AddDependency(e);
        e.AddDependency(a);   // цикл
        return (g, a);
    }

    [Fact]
    public void Bfs_YieldReturn_VisitsAllOnce()
    {
        var (_, root) = BuildGraph();
        var visited = new ServiceGraphBfs(root).Select(n => n.Id).ToList();

        Assert.Equal(new[] { "A", "B", "C", "D", "E" }, visited);
    }

    [Fact]
    public void Dfs_YieldReturn_VisitsAllOnce()
    {
        var (_, root) = BuildGraph();
        var visited = new ServiceGraphDfs(root).Select(n => n.Id).ToList();

        Assert.Equal(new[] { "A", "B", "D", "E", "C" }, visited);
    }

    [Fact]
    public void NestedForeach_Works_WithoutSharedState()
    {
        var (_, root) = BuildGraph();

        var pairs = new List<string>();
        foreach (var service in new ServiceGraphDfs(root))
            foreach (var dependency in new ServiceGraphBfs(service))
                pairs.Add($"{service}->{dependency}");

        // 5 сервисов × 5 достижимых = 25 пар, вложенность не сбивается
        Assert.Equal(25, pairs.Count);
        Assert.Contains("A->A", pairs);
        Assert.Contains("E->E", pairs);
    }

    [Fact]
    public void ManualIterator_MoveNext_Current_Reset_Work()
    {
        var (_, root) = BuildGraph();
        var it = new ServiceGraphBfsManual(root);

        Assert.True(it.MoveNext());
        Assert.Equal("A", it.Current.Id);

        it.Reset();
        Assert.True(it.MoveNext());
        Assert.Equal("A", it.Current.Id);
    }

    [Fact]
    public void CycleFinder_FindsCycle()
    {
        var (_, root) = BuildGraph();
        var cycle = CycleFinder.FindCycle(root);

        Assert.NotNull(cycle);
        Assert.Contains("A", cycle!);
        Assert.Equal(cycle[0], cycle[^1]);   // замкнут
    }

        // ── Пункт: пустой граф ──
    [Fact]
    public void EmptyGraph_NoNodes_EmptyTraversal()
    {
        var g = new ServiceGraph();
        var root = g.GetOrAdd("A");   // узел без зависимостей

        var visited = new ServiceGraphBfs(root).Select(n => n.Id).ToList();
        Assert.Single(visited);
        Assert.Equal("A", visited[0]);
    }

    // ── Пункт: граф из одной вершины ──
    [Fact]
    public void SingleNodeGraph_VisitsOnlyThatNode()
    {
        var g = new ServiceGraph();
        var only = g.GetOrAdd("solo");

        Assert.Equal(new[] { "solo" }, new ServiceGraphBfs(only).Select(n => n.Id));
        Assert.Equal(new[] { "solo" }, new ServiceGraphDfs(only).Select(n => n.Id));
    }

    // ── Пункт: обе реализации итератора дают одинаковую последовательность ──
    [Fact]
    public void ManualAndYieldIterators_ProduceSameSequence()
    {
        var (_, root) = BuildGraph();

        var yieldSeq  = new ServiceGraphBfs(root).Select(n => n.Id).ToList();
        var manualSeq = new List<string>();
        var it = new ServiceGraphBfsManual(root);
        while (it.MoveNext())
            manualSeq.Add(it.Current.Id);

        Assert.Equal(yieldSeq, manualSeq);
    }
}