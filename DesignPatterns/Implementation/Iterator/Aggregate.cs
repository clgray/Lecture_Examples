namespace Implementation.Iterator
{
	/// <summary>
	/// The 'Aggregate' abstract class
	/// </summary>
	abstract class Aggregate
	{
		public abstract Iterator CreateIterator();
		public abstract int Count { get; }
		public abstract object this[int index] { get; set; }
	}
}