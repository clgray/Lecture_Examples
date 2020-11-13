using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Strategy
{
	public class LogImporter : ILogImporter
	{
		public List<LogEntry> GetLogs()
		{
			return new List<LogEntry>();
		}
	}
	public interface ILogImporter
	{
		List<LogEntry> GetLogs();
	}

	class LogProcessor
	{
		
		private readonly ILogImporter _logImporter;
		public LogProcessor(ILogImporter logImporter)
		{
			_logImporter = logImporter;
		}

		public void ProcessLogs()
		{
			foreach (var logEntry in _logImporter.GetLogs())
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
