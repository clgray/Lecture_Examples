namespace LogHub.Graphs;

public static class CycleFinder
{
    public static List<string>? FindCycle(ServiceNode root)
    {
        var visited = new HashSet<string>();
        var path = new List<string>();
        return Dfs(root, visited, path);
    }

    private static List<string>? Dfs(ServiceNode node, HashSet<string> visited, List<string> path)
    {
        if (path.Contains(node.Id))
        {
            var idx = path.IndexOf(node.Id);
            var cycle = path.Skip(idx).ToList();
            cycle.Add(node.Id);
            return cycle;
        }

        if (!visited.Add(node.Id)) return null;

        path.Add(node.Id);

        foreach (var dep in node.Dependencies)
        {
            var cycle = Dfs(dep, visited, path);
            if (cycle is not null) return cycle;
        }

        path.RemoveAt(path.Count - 1);
        return null;
    }
}