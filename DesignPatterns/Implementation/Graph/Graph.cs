using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Implementation.Graph
{
	public class Graph : IEnumerable, IEnumerator
	{
		readonly Stack<Node> _mem = new Stack<Node>();
		readonly HashSet<int> _visited = new HashSet<int>();
		Node _current;
		readonly Node[] _graph;
		readonly Node _startNode;

		public Graph(Node[] graph, Node startNode)
		{
			_graph = graph;
			_startNode = startNode;
		}

		public IEnumerator GetEnumerator()
		{
			Reset();
			return this;
		}

		public bool MoveNext()
		{
			while (true)
			{
				if (_mem.Count > 0)
				{
					var node = _mem.Pop();
					if (_visited.Contains(node.Id)) continue;

					_current = node;
					_visited.Add(_current.Id);

					if (_current.ChildNodes != null)
					{
						foreach (var child in _current.ChildNodes.Reverse())
						{
							if (!_visited.Contains(child.Id))
							{
								_mem.Push(child);
							}
						}
					}
					return true;
				}
				return false;
			}
		}

		public void Reset()
		{
			_mem.Clear();
			_mem.Push(_startNode);
			_visited.Clear();
		}

		public object Current => _current;
	}
}