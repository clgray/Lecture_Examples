using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Strategy
{
	public class FileLogImporter : ILogImporter
	{
		public List<LogEntry> GetLogs()
		{
			return new List<LogEntry>()
			{
				new LogEntry
				{
					Severity = LogSeverity.Debug, Date = DateTime.Now, Message = "Вызвали метод. Всё хорошо",
				}
			};
		}
	}

	public class SystemLogImporter : ILogImporter
	{
		public List<LogEntry> GetLogs()
		{
			return new List<LogEntry>()
			{
				new LogEntry {Severity = LogSeverity.Critical, Date = DateTime.Now, Message = "Всё плохо"},
				new LogEntry
				{
					Severity = LogSeverity.Debug, Date = DateTime.Now, Message = "Запустилась функция А()",
				},
				new LogEntry
				{
					Severity = LogSeverity.Error, Date = DateTime.Now, Message = "Неизвестная ошибка",
				}
			};
		}
	}

	public interface ILogImporter
	{
		List<LogEntry> GetLogs();
	}

	public class LogProcessor
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
			logMapActions[logEntry.Severity](logEntry);
		}

		private static void WriteConsoleLogEntry(LogEntry logEntry, ConsoleColor color)
		{
			var previousForegroundColor = Console.ForegroundColor;
			Console.ForegroundColor = color;
			Console.WriteLine($"Date {logEntry.Date}, Severity {logEntry.Severity}, Massage:{logEntry.Message}");
			Console.ForegroundColor = previousForegroundColor;
		}

		private Dictionary<LogSeverity, Action<LogEntry>> logMapActions = new Dictionary<LogSeverity, Action<LogEntry>>()
			{
			{
				LogSeverity.Critical, entry => { WriteConsoleLogEntry(entry, ConsoleColor.Red); }
			},
			{
				LogSeverity.Debug, entry => { WriteConsoleLogEntry(entry, ConsoleColor.Gray); }

			},
			{
				LogSeverity.Error, entry => { WriteConsoleLogEntry(entry, ConsoleColor.DarkMagenta); }

			}

			};

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
		public string Message { get; set; }
		public DateTime Date { get; set; }
		public LogSeverity Severity { get; set; }

		public static LogEntry Parse(string line)
		{
			throw new NotImplementedException();
		}
	}

	public enum LogSeverity
	{
		Debug = 0,
		Info = 1,
		Warning = 2,
		Error = 3,
		Critical = 4
	}

	//public class ExceptionLogEntry
	//{
	//	public static ExceptionLogEntry Parse(string line)
	//	{
	//		throw new NotImplementedException();
	//	}
	//}
}