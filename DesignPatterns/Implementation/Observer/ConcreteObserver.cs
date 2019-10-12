using System;

namespace Implementation.Observer
{
	class ConcreteObserver : IObserver
	{
		string name;


		string state;

		string gap;

		public ConcreteObserver(IObservable observable, string name, string gap)
		{
			this.name = name;
			this.gap = gap;
			//observable.AddObserver(this);
			observable.OnUpdate += Update;
		}

		public void Update(string subjectState)
		{
			state = subjectState;
			Console.WriteLine(gap + name + ": " + state);
		}
	}
}