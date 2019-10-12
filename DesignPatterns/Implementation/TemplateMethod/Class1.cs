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
}
