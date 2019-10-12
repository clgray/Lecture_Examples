using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Strategy
{
	class LogProcessor
	{
		private readonly Func<List<LogEntry>> _logImporter;
		public LogProcessor(Func<List<LogEntry>> logImporter)
		{
			_logImporter = logImporter;
		}

		public void ProcessLogs()
		{
			foreach (var logEntry in _logImporter.Invoke())
			{
				SaveLogEntry(logEntry);
			}
		}

		private void SaveLogEntry(LogEntry logEntry)
		{
			throw new NotImplementedException();
		}

		// Остальные методы пропущены...
	}

	//public class SimpleLogEntry
	//{
	//	public static SimpleLogEntry Parse(string line)
	//	{
	//		throw new NotImplementedException();
	//	}
	//}
	public class LogEntry
	{
		public static LogEntry Parse(string line)
		{
			throw new NotImplementedException();
		}
	}
	//public class ExceptionLogEntry
	//{
	//	public static ExceptionLogEntry Parse(string line)
	//	{
	//		throw new NotImplementedException();
	//	}
	//}
}
