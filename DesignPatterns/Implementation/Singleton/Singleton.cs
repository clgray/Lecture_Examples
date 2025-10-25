using System;
using System.Diagnostics;
using System.Threading;

namespace Implementation.Singleton
{
	public class DemoSingleton
	{
		private static DemoSingleton _instance;

		private DemoSingleton()
		{
		}

		public static DemoSingleton Instance()
		{
			if (_instance == null)
			{
				lock (lockObject)
				{
					if (_instance == null)
						_instance = new DemoSingleton();
				}
			}
		
			return _instance;
		}

		public static DemoSingleton Instance1()
		{
			return _instance;
		}

		public void IncCounter()
		{
			Interlocked.Increment(ref _counter);
			// lock (this)
			// {
			// 	_counter++;
			// }

		}
		public int GetCounter()
		{ 
			return _counter;
		}
		private int _counter = 0;
		private static object lockObject = new object();
	}

	public class DemoSingletonApp
	{
		static public void Run()
		{
			var threadsCount = 1000;
			var threads = new Thread[threadsCount];

			var sw = new Stopwatch();
			sw.Start();

			for (int i = 0; i < threadsCount; i++)
			{
				threads[i] = new Thread(Start);
				threads[i].Start();
			}

			for (int i = 0; i < threadsCount; i++)
			{
				threads[i].Join();
			}
			sw.Stop();
			Console.WriteLine(sw.Elapsed);
			Console.WriteLine(DemoSingleton.Instance().GetCounter());
		}


		static void Start()
		{
			for (int i = 0; i < 10000; i++)
			{
				DemoSingleton.Instance().IncCounter();
			}
		}
	}
	}