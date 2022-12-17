﻿using System;
using Implementation.Strategy;

namespace Implementation.Adapter
{
	public static class AdapterRunner
	{
		public static void Run()
		{
			var adapter = new SqlServerLogSaverAdapter();
			var client = new LogSaverClient(adapter);
			client.Save(new LogEntry {Date = DateTime.Now, Severity = LogSeverity.Info, Message = "AdapterRunner Run"});
			client.Save(new LogEntry { Date = DateTime.Now, Severity = LogSeverity.Critical, Message = "AdapterRunner critical error" });
		}
	}
}