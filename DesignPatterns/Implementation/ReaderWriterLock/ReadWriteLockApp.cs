using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Implementation.ReaderWriterLock
{
	class ReadWriteLockApp
	{
		public static void Run()
		{
			var readers = new List<Thread>();
			var writers = new List<Thread>();
			var source = new Source();
			for (int i = 0; i < 50; i++)
			{
				readers.Add(new Thread(() => source.Read() ));
				if (i<10)
				writers.Add(new Thread(() => source.Write()));
			}
			for (int i = 0; i < 50; i++)
			{
				if (i < 10)
					writers[i].Start();
				readers[i].Start();
			}
			for (int i = 0; i < 50; i++)
			{
				if (i < 10)
					writers[i].Join();
				readers[i].Join();
			}

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
				//lock (rw)
				//{
					var s = _list.Sum(x => x);
				rw.ExitReadLock();

					Console.WriteLine("{0}, - read thread {1}", s, Thread.CurrentThread.ManagedThreadId);
				//}
			}

		public void Write()
			{
				Thread.Sleep(0);
				rw.EnterWriteLock();
				//lock (rw)
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
