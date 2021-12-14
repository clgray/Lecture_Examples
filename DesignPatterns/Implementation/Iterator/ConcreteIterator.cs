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
		public override void First()
		{
			_current = 0;
		}

		// Gets next iteration item
		public override void Next()
		{
			if (_current < _aggregate.Count - 1)
			{
				_current++;
			}
		}

		// Gets current iteration item
		public override object CurrentItem()
		{
			return _aggregate[_current];
		}

		// Gets whether iterations are complete
		public override bool IsDone()
		{
			return _current >= _aggregate.Count - 1;
		}
	}
}