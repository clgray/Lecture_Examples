namespace Implementation.Iterator
{
	/// <summary>
	/// The 'ConcreteIterator' class
	/// </summary>
	class ConcreteIterator : Iterator
	{
		private readonly Aggregate _aggregate;
		private int _current;

		// Constructor
		public ConcreteIterator(Aggregate aggregate)
		{
			this._aggregate = aggregate;
		}

		// Gets first iteration item
		public override object First()
		{
			_current = 0;
			return _aggregate[_current];
		}

		// Gets next iteration item
		public override object Next()
		{
			object ret = null;

			_current++;

			if (_current < _aggregate.Count)
			{
				ret = _aggregate[_current];
			}

			return ret;
		}

		// Gets current iteration item
		public override object CurrentItem()
		{
			return _aggregate[_current];
		}

		// Gets whether iterations are complete
		public override bool IsDone()
		{
			return _current >= _aggregate.Count;
		}
	}
}