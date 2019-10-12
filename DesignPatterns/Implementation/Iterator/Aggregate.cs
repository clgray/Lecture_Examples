namespace Implementation.Iterator
{
	/// <summary>
	/// The 'Aggregate' abstract class
	/// </summary>
	abstract class Aggregate
	{
		public abstract Iterator CreateIterator();
		public abstract int Count { get; protected set; }
		public abstract object this[int index] { get; set; }
	}
}