using System;

namespace Implementation.Visitor
{

	//public class DatabaseLogSaver
	//{
	//	public void SaveLogEntry(LogEntry logEntry)
	//	{
	//		var exception = logEntry as ExceptionLogEntry;
	//		if (exception != null)
	//		{
	//			SaveException(exception);
	//		}

	//		else
	//		{
	//			var simpleLogEntry = logEntry as SimpleLogEntry;
	//			 if (simpleLogEntry != null)
	//				SaveSimpleLogEntry(simpleLogEntry);


	//			throw new InvalidOperationException("Unknown  log  entry type");
	//		}
	//	}


	//		private void SaveSimpleLogEntry(SimpleLogEntry logEntry) {...}
	//		private void SaveException(ExcetpionLogEntry exceptionLogEntry) {...}
	//	}
	public class DatabaseLogSaver : ILogEntryVisitor
	{
		public void SaveLogEntry(LogEntry logEntry)
		{
			logEntry.Accept(this);
		}

		void ILogEntryVisitor.Visit(ExceptionLogEntry exceptionLogEntry)
		{
			Console.WriteLine($"SaveException to Database - {exceptionLogEntry.Message}");
		}

		void ILogEntryVisitor.Visit(SimpleLogEntry simpleLogEntry)
		{
			Console.WriteLine($"SaveSimple to Database - {simpleLogEntry.Message}");
		}

		public void Visit(CriticalLogEntry criticalLogEntry)
		{
			Console.WriteLine($"SaveCritical to Database - {criticalLogEntry.Message}");
		}

	}

	public class KibanaLogSaver : ILogEntryVisitor
	{
		public void SaveLogEntry(LogEntry logEntry)
		{
			logEntry.Accept(this);
		}

		void ILogEntryVisitor.Visit(ExceptionLogEntry exceptionLogEntry)
		{
			Console.WriteLine($"SaveException to Kibana - {exceptionLogEntry.Message}");
		}

		void ILogEntryVisitor.Visit(SimpleLogEntry simpleLogEntry)
		{
			Console.WriteLine($"SaveSimple to Kibana - {simpleLogEntry.Message}");
		}

		public void Visit(CriticalLogEntry criticalLogEntry)
		{
			Console.WriteLine($"SaveCritical to Kibana - {criticalLogEntry.Message}");
		}
	}
}