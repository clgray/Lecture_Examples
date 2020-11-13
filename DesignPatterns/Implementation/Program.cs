using System;
using System.Collections.Generic;
using System.Diagnostics;
using Implementation.Composite;
using Implementation.Enumerator;
using Implementation.Facade;
using Implementation.Graph;
using Implementation.Iterator;
using Implementation.Observer;
using Implementation.Proxy;
using Implementation.ReaderWriterLock;
using Implementation.Singleton;
using Implementation.Strategy;
using Implementation.Visitor;

namespace Implementation
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			FacadeProgram.Run();
			//var log = new LogProcessor(new LogImporter());
			//log.ProcessLogs();
		}
	}
}