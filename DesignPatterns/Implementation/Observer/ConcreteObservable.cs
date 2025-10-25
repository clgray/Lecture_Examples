using System;
using System.Collections.Generic;
using System.Threading;

namespace Implementation.Observer
{
	public delegate void Update(string s);
	class ConcreteObservable : IObservable
	{
		public event Update OnUpdate;
		public string SubjectState { get; set; }
		private List<IObserver> Observers { get; set; }

		private Simulator simulator;

		private const int speed = 5000;

		public ConcreteObservable()
		{
			Observers = new List<IObserver>();
			simulator = new Simulator();
		}

		public void AddObserver(IObserver observer)
		{
			Observers.Add(observer);
		}

		public void RemoveObserver(IObserver observer)
		{
			Observers.Remove(observer);
		}

		public void NotifyObservers(string s)
		{
			//foreach (var observer in Observers)
			//{
			//	observer.Update(s);
			//}
			OnUpdate?.Invoke(s);
		}

		public void Go()
		{
			new Thread(Run).Start();
		}

		void Run()
		{
			foreach (string s in simulator)
			{
				Console.WriteLine("Observable element state: " + s);
				SubjectState = s;
				NotifyObservers(s);
				Thread.Sleep(speed); // milliseconds
			}
		}
	}
}