using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Implementation.Strategy;

namespace Implementation.TemplateMethod
{
	public abstract class LogReader
	{
		private int _currentPosition;

		// Метод ReadLogEntry невиртуальный: определяет алгоритм импорта
		public IEnumerable<LogEntry> ReadLogEntry()
		{
			return ReadEntries(ref _currentPosition).Select(ParseLogEntry);
		}

		protected abstract IEnumerable<string> ReadEntries(ref int position);

		protected abstract LogEntry ParseLogEntry(string stringEntry);
	}

	public class LogReaderImpl : LogReader
	{
		protected override IEnumerable<string> ReadEntries(ref int position)
		{
			var s = new SystemLogImporter();
			return s.GetLogs().Select(x=>x.Message);
		}

		protected override LogEntry ParseLogEntry(string stringEntry)
		{
			Random _r = new Random(); 
			return new LogEntry()
			{
				Date = DateTime.Now,
				Message = stringEntry,
				Severity = (LogSeverity) _r.Next(4)
			};
		}
	}
}
