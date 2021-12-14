using System;

namespace Implementation.Observer
{
	class ObserverApp
	{
		public static void Run()
		{
			ConcreteObservable concreteObservable = new ConcreteObservable();
			ConcreteObserver observer0 = new ConcreteObserver(concreteObservable, "Left", "\t");
			ConcreteObserver observer1 = new ConcreteObserver(concreteObservable, "Center", "\t\t");
			ConcreteObserver observer2 = new ConcreteObserver(concreteObservable, "Right", "\t\t\t\t");
			concreteObservable.Go();

			// Wait for user
			Console.Read();
		}
	}
}
