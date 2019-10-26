using System.Collections;
using System.Collections.Generic;

namespace Implementation.Graph
{
	public class Graph : IEnumerable, IEnumerator
	{
		readonly Stack<Node> _mem = new Stack<Node>();
		readonly HashSet<int> _visited = new HashSet<int>();
		Node _current;
		readonly Node[] _graph;
		readonly int _startId;

		public Graph(Node[] graph, int startId)
		{
			_graph = graph;
			_startId = startId;
		}

		public IEnumerator GetEnumerator()
		{
			Reset();
			return this;
		}

		public bool MoveNext()
		{
			if (_current == null)
			{
				_current = _graph[_startId];
				_visited.Add(_current.Id);

				return true;
			}
			if (_current.ChildNodes != null)
			{
				foreach (var child in _current.ChildNodes)
				{
					if (!_visited.Contains(child.Id))
					{
						_mem.Push(child);
						_visited.Add(child.Id);
					}
				}
			}

			if (_mem.Count > 0)
			{
				_current = _mem.Pop();
				return true;
			}

			return false;
		}

		public void Reset()
		{
			
			_visited.Clear();
		}

		public object Current
		{
			get { return _current; }
		}
	}
}