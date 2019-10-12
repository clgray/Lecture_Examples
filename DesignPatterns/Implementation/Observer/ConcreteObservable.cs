﻿using System;
using System.Collections.Generic;
using System.Threading;

namespace Implementation.Observer
{
	public delegate void Update(string s);
	class ConcreteObservable : IObservable
	{
		public event Update OnUpdate;
		public string SubjectState { get; set; }
		public List<IObserver> Observers { get; private set; }

		private Simulator simulator;

		private const int speed = 200;

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
				Console.WriteLine("Observer: " + s);
				SubjectState = s;
				NotifyObservers(s);
				Thread.Sleep(speed); // milliseconds
			}
		}
	}
}