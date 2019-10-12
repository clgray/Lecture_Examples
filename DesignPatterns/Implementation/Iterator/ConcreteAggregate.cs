using System.Collections;

namespace Implementation.Iterator
{
	/// <summary>
	/// The 'ConcreteAggregate' class
	/// </summary>
	class ConcreteAggregate : Aggregate
	{
		private readonly ArrayList _items = new ArrayList();

		public override Iterator CreateIterator()
		{
			return new ConcreteIterator(this);
		}

		// Gets item count
		public override int Count
		{
			get { return _items.Count; }
			protected set { }
		}

		// Indexer
		public override object this[int index]
		{
			get { return _items[index]; }
			set { _items.Insert(index, value); }
		}
	}
}