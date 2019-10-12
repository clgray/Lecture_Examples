namespace Implementation.Observer
{
	interface IObservable
	{
		event Update OnUpdate;
		void AddObserver(IObserver observer);
		void RemoveObserver(IObserver observer);
		void NotifyObservers(string s);
	}
}