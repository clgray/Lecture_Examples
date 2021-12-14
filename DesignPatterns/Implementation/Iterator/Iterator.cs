namespace Implementation.Iterator
{
	/// <summary>
	/// The 'Iterator' abstract class
	/// </summary>
	abstract class Iterator
	{
		public abstract void First();
		public abstract void Next();
		public abstract bool IsDone();
		public abstract object CurrentItem();
	}
}