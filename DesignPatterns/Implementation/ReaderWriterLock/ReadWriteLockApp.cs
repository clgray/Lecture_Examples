using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Implementation.Strategy;

namespace Implementation.ReaderWriterLock
{
	class ReadWriteLockApp
	{
		public static void Run()
		{
			Stopwatch sw = new Stopwatch();
			var readers = new List<Thread>();
			var writers = new List<Thread>();
			var source = new Source();

			for (int i = 0; i < 50; i++)
			{
				readers.Add(new Thread(() => source.Read()));
				if (i % 5 == 0)
					writers.Add(new Thread(() => source.Write()));
			}

			sw.Start();

			for (int i = 0; i < 50; i++)
			{
				if (i % 5 == 0)
					writers[i / 5].Start();
				readers[i].Start();
			}

			for (int i = 0; i < 50; i++)
			{
				if (i % 5 == 0)
					writers[i/5].Join();
				readers[i].Join();
			}

			sw.Stop();
			Console.WriteLine(sw.Elapsed);
		}

		class Source
		{
			//static ReadWriteLock rw = new ReadWriteLock();

			static ReaderWriterLockSlim rw = new ReaderWriterLockSlim();
			private List<long> _list = new List<long>();

			public Source()
			{
				var rn = new Random();
				for (int i = 0; i < 10000000; i++)
				{
					_list.Add(rn.Next(10000));
				}
			}

			public void Read()
			{
				Thread.Sleep(0);
				rw.EnterReadLock();
				long s = 0;
				//lock (this)
				{
					s = _list.Sum(x => x);
				}
				rw.ExitReadLock();

				Console.WriteLine("{0}, - read thread {1}", s, Thread.CurrentThread.ManagedThreadId);
				//}
			}

			public void Write()
			{
				Thread.Sleep(0);
				rw.EnterWriteLock();
				//lock (this)
				{
					var rn = new Random();
					_list[rn.Next(_list.Count)] = rn.Next(10000);
				}
				rw.ExitWriteLock();
				Console.WriteLine("write thread {0}", Thread.CurrentThread.ManagedThreadId);
			}
		}
	}
}