using System;
using System.Collections.Generic;
using System.Text;
using Implementation.Visitor;
using LogEntry = Implementation.Strategy.LogEntry;

namespace Implementation.Adapter
{
	public class SqlServerLogSaverAdapter : ILogSaver
	{
		private readonly SqlServerLogSaver _sqlServerLogSaver =
			new SqlServerLogSaver();

		public void Save(LogEntry logEntry)
		{
			_sqlServerLogSaver.Save(logEntry.Date, logEntry.Severity.ToString(), logEntry.Message);
		}
	}
}