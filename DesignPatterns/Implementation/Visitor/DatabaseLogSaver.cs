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
			SaveException(exceptionLogEntry);
		}

		void ILogEntryVisitor.Visit(SimpleLogEntry simpleLogEntry)
		{
			SaveSimpleLogEntry(simpleLogEntry);
		}

		public void Visit(CriticalLogEntry criticalLogEntry)
		{
			throw new System.NotImplementedException();
		}

		private void SaveException(ExceptionLogEntry logEntry)
		{
			
		}

		private void SaveSimpleLogEntry(SimpleLogEntry logEntry)
		{
			
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
			SaveException(exceptionLogEntry);
		}

		void ILogEntryVisitor.Visit(SimpleLogEntry simpleLogEntry)
		{
			SaveSimpleLogEntry(simpleLogEntry);
		}

		public void Visit(CriticalLogEntry criticalLogEntry)
		{
			throw new System.NotImplementedException();
		}

		private void SaveException(ExceptionLogEntry logEntry)
		{

		}

		private void SaveSimpleLogEntry(SimpleLogEntry logEntry)
		{

		}
	}
}