namespace LogHub.Graphs;

public static class ServiceGraphDemo
{
    public static void Run()
    {
        var graph = new ServiceGraph();
        var a = graph.GetOrAdd("A");
        var b = graph.GetOrAdd("B");
        var c = graph.GetOrAdd("C");
        var d = graph.GetOrAdd("D");
        var e = graph.GetOrAdd("E");

        a.AddDependency(b).AddDependency(c);
        b.AddDependency(d);
        c.AddDependency(d);
        d.AddDependency(e);
        e.AddDependency(a);   // цикл: A → B → D → E → A

        Console.WriteLine("=== DFS (yield) ===");
        foreach (var s in new ServiceGraphDfs(a))
            Console.WriteLine(s);

        Console.WriteLine("=== BFS (yield) ===");
        foreach (var s in new ServiceGraphBfs(a))
            Console.WriteLine(s);

        Console.WriteLine("=== Вложенные циклы (п.6) ===");
        foreach (var service in new ServiceGraphDfs(a))
        {
            foreach (var dependency in new ServiceGraphBfs(service))
                Console.WriteLine($"{service} -> {dependency}");
        }

        Console.WriteLine("=== Цикл (п.7) ===");
        var cycle = CycleFinder.FindCycle(a);
        Console.WriteLine(cycle is null ? "циклов нет" : string.Join(" -> ", cycle));
    }
}