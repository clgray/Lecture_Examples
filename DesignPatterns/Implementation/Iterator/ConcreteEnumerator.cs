using System.Collections;
using System.Collections.Generic;

namespace Implementation.Iterator
{
	class ConcreteEnumerator : IEnumerator<object>
	{
		private readonly Aggregate _aggregate;
		private int _current = -1;

		public ConcreteEnumerator(Aggregate aggregate)
		{
			_aggregate = aggregate;
		}
		public bool MoveNext()
		{
			if (_current < _aggregate.Count - 1)
			{
				_current++;
				return true;
			}

			return false;
		}

		public void Reset()
		{
			_current = -1;
		}
		public object Current => _aggregate[_current];

		object IEnumerator.Current => Current;

		public void Dispose()
		{
			
		}
	}
}