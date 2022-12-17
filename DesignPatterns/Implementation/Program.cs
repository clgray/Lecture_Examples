using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using Implementation.Abstract_Factory;
using Implementation.Adapter;
using Implementation.Composite;
using Implementation.Decorator;
using Implementation.Enumerator;
using Implementation.Facade;
using Implementation.FactoryMethod;
using Implementation.Graph;
using Implementation.heap;
using Implementation.Iterator;
using Implementation.Observer;
using Implementation.Proxy;
using Implementation.ReaderWriterLock;
using Implementation.Singleton;
using Implementation.Strategy;
using Implementation.TemplateMethod;
using Implementation.Visitor;
using Implementation.ChainOfResponsibility;

namespace Implementation
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			BikeShop.UpgradeBike();
		}
	}
}