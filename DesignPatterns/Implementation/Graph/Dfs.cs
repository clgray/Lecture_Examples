using System;
using System.Collections.Generic;
using System.ComponentModel;
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
		}

		readonly HashSet<int> _visited;
		private IMemory _mem = new QueueMemory();

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
			_mem.Add(node);
			_visited.Clear();
			DfsStack();
		}

		public void DfsStack()
		{
			while (!_mem.IsEmpty())
			{
				var node = _mem.Get();
				if (_visited.Contains(node.Id)) continue;
				Console.WriteLine(node.Id);
				_visited.Add(node.Id);

				if (node.ChildNodes != null)
				{
					foreach (var child in node.ChildNodes.Reverse())
					{
						if (!_visited.Contains(child.Id))
						{
							_mem.Add(child);
						}
					}
				}
			}
		}
	}

	interface IMemory
	{
		void Add(Node node);
		Node Get();
		bool IsEmpty();
	}

	class StackMemory : IMemory
	{
		private Stack<Node> _mem = new Stack<Node>();
		public void Add(Node node)
		{
			_mem.Push(node);
		}

		public Node Get()
		{
			return _mem.Pop();
		}

		public bool IsEmpty()
		{
			return _mem.Count == 0;
		}
	}

	class QueueMemory : IMemory
	{
		private Queue<Node> _mem = new Queue<Node>();
		public void Add(Node node)
		{
			_mem.Enqueue(node);
		}

		public Node Get()
		{
			return _mem.Dequeue();
		}

		public bool IsEmpty()
		{
			return _mem.Count == 0;
		}
	}
}