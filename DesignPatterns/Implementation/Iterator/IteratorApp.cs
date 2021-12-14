using System;
using System.Linq;

namespace Implementation.Iterator
{
	public class IteratorApp
	{
		/// <summary>
		/// Entry point into console application.
		/// </summary>
		public static void Run()
		{
			ConcreteAggregate a = new ConcreteAggregate();
			a[0] = "Item A";
			a[1] = "Item B";
			a[2] = "Item C";
			a[3] = "Item D";

			// Create Iterator and provide aggregate
			Iterator i = a.CreateIterator();

			Console.WriteLine("Iterating over collection:");

			i.First();
			while (!i.IsDone())
			{
				Console.WriteLine(i.CurrentItem());
				i.Next();
			}

			Console.WriteLine(i.CurrentItem());
			// Wait for user
			Console.ReadKey();
		}

		public static void RunEnumerable()
		{
			var a = new EnumerableAggregate();
			a[0] = "Item A";
			a[1] = "Item B";
			a[2] = "Item C";
			a[3] = "Item D";

			//Iterator i = a.CreateIterator();

			//Console.WriteLine("Iterating over collection:");

			//i.First();
			//while (!i.IsDone())
			//{
			//	Console.WriteLine(i.CurrentItem());
			//	i.Next();
			//}
			//Console.WriteLine(i.CurrentItem());

			//var enumerator = a.GetEnumerator();
			//enumerator.Reset();
			//while (enumerator.MoveNext())
			//{
			//	Console.WriteLine(enumerator.Current);
			//}

			//foreach (var item in a)
			//{
			//	Console.WriteLine(item);
			//}

			a
				.Skip(1)
				.Take(2)
				.Cast<string>()
				.Where(x => !x.Contains("C"))
				.Select(x =>
				{
					Console.WriteLine(x);
					return x;
				})
				.ToArray();

			// Wait for user
			Console.ReadKey();
		}
	}
}