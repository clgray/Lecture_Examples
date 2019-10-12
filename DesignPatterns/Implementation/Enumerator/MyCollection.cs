using System.Collections;

namespace Implementation.Enumerator
{
	internal class MyCollection : IEnumerable, IEnumerator
	{
		private readonly ArrayList _items = new ArrayList();
		private int _current;
		public MyCollection(object[] items)
		{
			_items.AddRange(items);
		}

		public IEnumerator GetEnumerator()
		{
			Reset();
			return this;
		}
		public bool MoveNext()
		{
			if (_items.Count > _current)
			{
				_current++;
				return true;
			}
			return false;
		}

		public void Reset()
		{
			_current = 0;
		}

		public object Current
		{
			get { return _items[_current - 1]; }
		}
	}

	internal class MyCollectionEnumerator : IEnumerator
	{
		private readonly ArrayList _items;
		private int _current;

		public MyCollectionEnumerator(ArrayList items)
		{
			_items = items;
			_current = 0;
		}

		public bool MoveNext()
		{
			if (_items.Count > _current)
			{
				_current++;
				return true;
			}
			return false;
		}

		public void Reset()
		{
			_current = 0;
		}

		public object Current
		{
			get { return _items[_current-1]; }
		}
	}
}