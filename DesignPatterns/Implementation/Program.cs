using System;
using System.Diagnostics;
using Implementation.Enumerator;
using Implementation.Graph;
using Implementation.Iterator;
using Implementation.Observer;
using Implementation.ReaderWriterLock;
using Implementation.Singleton;
using Implementation.Visitor;

namespace Implementation
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			//Stopwatch sw = new Stopwatch();
			//sw.Start();
			//ReadWriteLockApp.Run();

			GraphApp.Run();

			//var app = new SingletonApp();
			//app.Run();

			//sw.Stop();
			//Console.WriteLine(sw.Elapsed);
		}
	}
}