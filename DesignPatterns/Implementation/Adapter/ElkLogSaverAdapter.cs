using Implementation.Strategy;

namespace Implementation.Adapter
{
	public class ElkLogSaverAdapter : ILogSaver
	{
		private readonly ElasticSearchLogSaver _elasticSearchLogSaver =
			new ElasticSearchLogSaver();

		public void Save(LogEntry logEntry)
		{
			_elasticSearchLogSaver.Save(
				new ElkLogEntry
				{
					Date = logEntry.Date,
					Severity = logEntry.Severity.ToString(),
					Message = logEntry.Message
				});
		}
	}
}