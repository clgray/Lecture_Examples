using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Graph
{
	class NewGraph
	{
		public void Bfs(Node node)
		{
			var queue = new Queue<Node>();
			var visited = new HashSet<Node>();
			queue.Enqueue(node);
			while (queue.Count > 0)
			{
				var element = queue.Dequeue();
				Console.WriteLine(element.Id);
				visited.Add(element);
				foreach (var child in element.ChildNodes)
				{
					if (!visited.Contains(child)) 
						queue.Enqueue(child);
				}
			}
		}


		public void Dfs(Node node)
		{
			Console.WriteLine(node.Id);
			foreach (var child in node.ChildNodes)
			{
				Dfs(child);
			}
		}

		public void DfsStack(Node node)
		{
			var stack = new Stack<Node>();
			var visited = new HashSet<Node>();
			stack.Push(node);
			while (stack.Count > 0)
			{
				var element = stack.Pop();
				Console.WriteLine(element.Id);
				visited.Add(element);
				foreach (var child in element.ChildNodes.Reverse())
				{
					if (!visited.Contains(child))
						stack.Push(child);
				}
			}
		}
	}
}