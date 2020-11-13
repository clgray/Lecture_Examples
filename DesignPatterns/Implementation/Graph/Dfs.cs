using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Graph
{
	class DfsGraph
	{
		private readonly Node[] _graph;

		public DfsGraph(Node[] graph)
		{
			_graph = graph;
			_visited = new HashSet<int>();
			_mem = new Stack<Node>();
		}

		readonly HashSet<int> _visited;
		private Stack<Node> _mem;

		public void Dfs(Node node)
		{
			Console.WriteLine(node.Id);
			_visited.Add(node.Id);

			if (node.ChildNodes != null)
			{
				foreach (var child in node.ChildNodes)
				{
					if (!_visited.Contains(child.Id))
					{
						Dfs(child);
					}
				}
			}
		}

		public void DfsStackStart(Node node)
		{
			_mem.Push(node);
			_visited.Clear();
			DfsStack();
		}

		public void DfsStack()
		{
			while (_mem.Count > 0)
			{
				var node = _mem.Pop();
				if (_visited.Contains(node.Id)) continue;
				Console.WriteLine(node.Id);
				_visited.Add(node.Id);

				if (node.ChildNodes != null)
				{
					foreach (var child in node.ChildNodes.Reverse())
					{
						if (!_visited.Contains(child.Id))
						{
							_mem.Push(child);
						}
					}
				}
			}
		}
	}
}