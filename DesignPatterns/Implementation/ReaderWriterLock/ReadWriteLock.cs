using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Implementation.ReaderWriterLock
{
	class ReadWriteLock
	{
		private static object _lockObj = new object();
		private int _readCount = 0;
		private bool _hasWrite = false;
		public void BeginRead()
		{
			Monitor.Enter(_lockObj);
			while (_hasWrite)
			{
				Monitor.Wait(_lockObj);
			}
			_readCount++;
			Monitor.Exit(_lockObj);
		}
		public void EndRead()
		{
			Monitor.Enter(_lockObj);
			_readCount--;
			if (_readCount == 0)
				Monitor.Pulse(_lockObj);

			Monitor.Exit(_lockObj);

		}
		public void BeginWrite()
		{

			Monitor.Enter(_lockObj);
			while (_hasWrite || _readCount != 0)
			{
				Monitor.Wait(_lockObj);
			}
			_hasWrite = true;

			Monitor.Exit(_lockObj);
		}
		public void EndWrite()
		{
			Monitor.Enter(_lockObj);
			_hasWrite = false;
			Monitor.PulseAll(_lockObj);
			Monitor.Exit(_lockObj);
		}
	}
}
