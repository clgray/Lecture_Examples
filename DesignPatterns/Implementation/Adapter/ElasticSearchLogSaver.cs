using System;

namespace Implementation.Adapter
{
	internal class ElasticSearchLogSaver
	{
		public void Save(ElkLogEntry logEntry)
		{
			Console.WriteLine($"ElkSaver {logEntry.Date} [{logEntry.Severity}] - {logEntry.Message}");
		}
	}
}