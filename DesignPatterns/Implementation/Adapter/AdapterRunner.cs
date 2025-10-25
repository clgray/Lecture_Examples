using System;
using Implementation.Strategy;

namespace Implementation.Adapter
{
	public static class AdapterRunner
	{
		public static void Run()
		{
			ILogSaver logSaver = new ElkLogSaverAdapter();
			var client = new LogSaverClient(logSaver);
			client.Save(new LogEntry {Date = DateTime.Now, Severity = LogSeverity.Info, Message = "AdapterRunner Run"});
			client.Save(new LogEntry { Date = DateTime.Now, Severity = LogSeverity.Critical, Message = "AdapterRunner critical error" });
		}
	}
}