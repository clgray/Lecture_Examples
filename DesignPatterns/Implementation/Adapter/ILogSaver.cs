using Implementation.Strategy;

namespace Implementation.Adapter
{
	public interface ILogSaver
	{
		void Save(LogEntry logEntry);
	}
}