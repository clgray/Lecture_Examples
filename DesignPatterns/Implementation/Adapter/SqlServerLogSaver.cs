using System;
using Implementation.Strategy;

namespace Implementation.Adapter
{
	internal class SqlServerLogSaver
	{
		public void Save(DateTime dateTime, string severity, string message)
		{
			Console.WriteLine($"SqlServer {dateTime} [{severity}] - {message}");
		}
	}
}